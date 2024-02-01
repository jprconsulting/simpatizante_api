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

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<SimpatizanteDTO>> GetById(int id)
        {
            var simpatizante = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (simpatizante == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SimpatizanteDTO>(simpatizante));
        }

        [HttpGet("obtener-simpatizantes-por-usuario-id/{usuarioId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorUsuarioId(int usuarioId)
        {
            var simpatizantes = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(u => u.Usuario)
                .Include(p => p.ProgramaSocial)
                .Where(s => s.Usuario.Id == usuarioId)
                .ToListAsync();

            if (!simpatizantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(simpatizantes));
        }

        [HttpGet("obtener-simpatizantes-por-candidato-id/{operadorId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorCandidatoId(int operadorId)
        {
            var simpatizantes = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(u => u.Usuario)
                .Include(p => p.ProgramaSocial)
                .Include(c => c.Operador)
                .Where(s => s.Operador.Id == operadorId)
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
                .Include(u => u.Usuario)
                .Include(p => p.ProgramaSocial)
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

            int usuarioId = int.Parse(User.FindFirst("usuarioId")?.Value);

            var simpatizante = mapper.Map<Simpatizante>(dto);

            simpatizante.Usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
            simpatizante.Seccion = await context.Secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            simpatizante.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            simpatizante.Estado = await context.Estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);
            simpatizante.Operador = await context.Operadores.SingleOrDefaultAsync(r => r.Id == dto.Operador.Id);

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
            int usuarioId = int.Parse(User.FindFirst("usuarioId")?.Value);

            mapper.Map(dto, simpatizante);
            simpatizante.Usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
            simpatizante.Seccion = await context.Secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            simpatizante.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            simpatizante.Estado = await context.Estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);
            simpatizante.Operador = await context.Operadores.SingleOrDefaultAsync(r => r.Id == dto.Operador.Id);

            if (dto.ProgramaSocial != null)
            {
                simpatizante.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            }

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