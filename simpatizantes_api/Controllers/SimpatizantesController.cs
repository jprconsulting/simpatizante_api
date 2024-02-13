using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/simpatizantes")]
    [ApiController]
    public class SimpatizantesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SimpatizantesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private async Task<bool> ValidarSimpatizantePorClaveElector(string claveElector)
        {
            return await context.Simpatizantes.AnyAsync(s => s.ClaveElector.Trim().ToLower() == claveElector.Trim().ToLower());
        }

        [HttpGet("validar-simpatizante-por-clave-elector/{claveElector}")]
        public async Task<ActionResult> GetValidarSimpatizantePorClaveElector(string claveElector)
        {
            var existeSimpatizante = await ValidarSimpatizantePorClaveElector(claveElector);

            if (existeSimpatizante)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<SimpatizanteDTO>> GetById(int id)
        {
            var simpatizante = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .Include(g => g.Genero)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (simpatizante == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SimpatizanteDTO>(simpatizante));
        }   

        [HttpGet("obtener-simpatizantes-por-operador-id/{operadorId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorOperadorId(int operadorId)
        {
            var simpatizantes = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .Include(c => c.Operador)
                .Include(g => g.Genero)
                .Where(s => s.Operador.Id == operadorId)
                .ToListAsync();

            if (!simpatizantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(simpatizantes));
        }

        [HttpGet("obtener-simpatizantes-por-candidato-id/{candidatoId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorCandidatoId(int candidatoId)
        {
            var simpatizantes = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .Include(c => c.Operador)
                .Include(g => g.Genero)
                .Where(s => s.Operador.Candidato.Id == candidatoId)
                .ToListAsync();

            if (!simpatizantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(simpatizantes));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetAll()
        {
            var simpatizantes = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(o => o.Operador)
                .Include(p => p.ProgramaSocial)
                .Include(g => g.Genero)
                .ToListAsync();

            if (!simpatizantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(simpatizantes));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(SimpatizanteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeSimpatizante = await ValidarSimpatizantePorClaveElector(dto.ClaveElector);

            if (existeSimpatizante)
            {
                return Conflict();
            }

            int usuarioId = int.Parse(User.FindFirst("usuarioId")?.Value);

            // Extraer año, mes y día de la clave del elector
            string claveElector = dto.ClaveElector;

            // Validar el formato de la clave del elector
            if (claveElector.Length != 18)
            {
                // Si la longitud de la clave del elector es incorrecta, establecer la fecha de nacimiento como DateTime.MinValue
                dto.FechaNacimiento = DateTime.MinValue;
            }
            else
            {
                // Procesar la clave del elector para extraer año, mes y día
                int yearPrefix = int.Parse(claveElector.Substring(6, 2));
                int year = yearPrefix <= 24 ? 2000 + yearPrefix : 1900 + yearPrefix;
                int month = int.Parse(claveElector.Substring(8, 2));
                int day = int.Parse(claveElector.Substring(10, 2));

                // Construir la fecha de nacimiento
                dto.FechaNacimiento = new DateTime(year, month, day);
            }

            var simpatizante = mapper.Map<Simpatizante>(dto);

            simpatizante.Seccion = await context.Secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            simpatizante.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            simpatizante.Estado = await context.Estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);
            simpatizante.Operador = await context.Operadores.SingleOrDefaultAsync(r => r.Id == dto.Operador.Id);
            simpatizante.Genero = await context.Generos.SingleOrDefaultAsync(g => g.Id == dto.Genero.Id);

            if (dto.ProgramaSocial != null)
            {
                simpatizante.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            }

            context.Add(simpatizante);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar el Simpatizante.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var simpatizante = await context.Simpatizantes.FindAsync(id);

            if (simpatizante == null)
            {
                return NotFound();
            }

            var tieneDependencias = await context.Visitas.AnyAsync(s => s.SimpatizanteId == id);

            if (tieneDependencias)
            {
                return StatusCode(502, new { error = "No se puede eliminar el simpatizante debido a dependencias existentes." });
            }

            context.Simpatizantes.Remove(simpatizante);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, SimpatizanteDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var simpatizante = await context.Simpatizantes.FindAsync(id);

            if (simpatizante == null)
            {
                return NotFound();
            }

            // Extraer año, mes y día de la clave del elector
            string claveElector = dto.ClaveElector;
            int yearPrefix = int.Parse(claveElector.Substring(6, 2));
            int year = yearPrefix <= 24 ? 2000 + yearPrefix : 1900 + yearPrefix;
            int month = int.Parse(claveElector.Substring(8, 2));
            int day = int.Parse(claveElector.Substring(10, 2));

            // Construir la fecha de nacimiento
            dto.FechaNacimiento = new DateTime(year, month, day);

            // Mapear el DTO actualizado al simpatizante
            mapper.Map(dto, simpatizante);
            simpatizante.Seccion = await context.Secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            simpatizante.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            simpatizante.Estado = await context.Estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);
            simpatizante.Operador = await context.Operadores.SingleOrDefaultAsync(r => r.Id == dto.Operador.Id);
            simpatizante.Genero = await context.Generos.SingleOrDefaultAsync(g => g.Id == dto.Genero.Id);

            if (dto.ProgramaSocial != null)
            {
                simpatizante.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            }

            // Actualizar el simpatizante en la base de datos
            context.Update(simpatizante);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimpatizanteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool SimpatizanteExists(int id)
        {
            return context.Simpatizantes.Any(s => s.Id == id);
        }

    }
}