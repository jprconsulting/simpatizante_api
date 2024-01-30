using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace simpatizantes_api.Controllers
{
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

        [HttpGet("obtener-simpatizantes-por-candidato-id/{candidatoId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorCandidatoId(int candidatoId)
        {
            var simpatizantes = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .Where(s => s.Candidato.Id == candidatoId)
                .ToListAsync();

            if (!simpatizantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(simpatizantes));
        }


        [HttpGet("obtener-simpatizantes-por-operador-id/{operadorId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorOperadorId(int operadorId)
        {
            var simpatizantes = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
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

            int currentUserRolId = int.Parse(User.FindFirst("rolId")?.Value);

            var simpatizante = mapper.Map<Simpatizante>(dto);
            simpatizante.Seccion = await context.Secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            simpatizante.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            simpatizante.Estado = await context.Estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);
            simpatizante.Operador = null;
            simpatizante.OperadorId = null;
            simpatizante.Candidato = null;
            simpatizante.CandidatoId = null;

            if (dto.ProgramaSocial != null)
            {
                simpatizante.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            }

            // Si es operador
            if (currentUserRolId == 2)
            {
                if (int.TryParse(User.FindFirst("operadorId")?.Value, out int parsedOperadorId))
                {
                    simpatizante.Operador = await context.Operadores.FirstOrDefaultAsync(o => o.Id == parsedOperadorId);
                }
            }

            // Si es candidato
            if (currentUserRolId == 3)
            {
                if (int.TryParse(User.FindFirst("candidatoId")?.Value, out int parsedCandidatoId))
                {
                    simpatizante.Candidato = await context.Candidatos.FirstOrDefaultAsync(c => c.Id == parsedCandidatoId);
                }
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
            int currentUserRolId = int.Parse(User.FindFirst("rolId")?.Value);

            mapper.Map(dto, simpatizante);
            simpatizante.Seccion = await context.Secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            simpatizante.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            simpatizante.Estado = await context.Estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);
            simpatizante.Operador = null;
            simpatizante.OperadorId = null;
            simpatizante.Candidato = null;
            simpatizante.CandidatoId = null;

            if (dto.ProgramaSocial != null)
            {
                simpatizante.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            }

            // Si es operador
            if (currentUserRolId == 2)
            {
                if (int.TryParse(User.FindFirst("operadorId")?.Value, out int parsedOperadorId))
                {
                    simpatizante.Operador = await context.Operadores.FirstOrDefaultAsync(o => o.Id == parsedOperadorId);
                }
            }

            // Si es candidato
            if (currentUserRolId == 3)
            {
                if (int.TryParse(User.FindFirst("candidatoId")?.Value, out int parsedCandidatoId))
                {
                    simpatizante.Candidato = await context.Candidatos.FirstOrDefaultAsync(c => c.Id == parsedCandidatoId);
                }
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