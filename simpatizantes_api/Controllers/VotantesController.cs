using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace simpatizantes_api.Controllers
{
    [Route("api/votantes")]
    [ApiController]
    public class VotantesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VotantesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<SimpatizanteDTO>> GetById(int id)
        {
            var votante = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (votante == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SimpatizanteDTO>(votante));
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
            var votante = await context.Simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .ToListAsync();

            if (!votante.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(votante));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(SimpatizanteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int currentUserRolId = int.Parse(User.FindFirst("rolId")?.Value);

            var votante = mapper.Map<Simpatizante>(dto);
            votante.Seccion = await context.Secciones.SingleOrDefaultAsync(i => i.Id == dto.Seccion.Id);
            votante.Municipio = await context.Municipios.SingleOrDefaultAsync(i => i.Id == dto.Municipio.Id);
            votante.Estado = await context.Estados.SingleOrDefaultAsync(c => c.Id == dto.Estado.Id);
            votante.Operador = null;
            votante.OperadorId = null;
            votante.Candidato = null;
            votante.CandidatoId = null;

            if (dto.ProgramaSocial != null)
            {
                votante.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(c => c.Id == dto.ProgramaSocial.Id);
            }

            // Si es operador
            if (currentUserRolId == 2)
            {
                if (int.TryParse(User.FindFirst("operadorId")?.Value, out int parsedOperadorId))
                {
                    votante.Operador = await context.Operadores.FirstOrDefaultAsync(o => o.Id == parsedOperadorId);
                }
            }

            // Si es candidato
            if (currentUserRolId == 3)
            {
                if (int.TryParse(User.FindFirst("candidatoId")?.Value, out int parsedCandidatoId))
                {
                    votante.Candidato = await context.Candidatos.FirstOrDefaultAsync(c => c.Id == parsedCandidatoId);
                }
            }

            context.Add(votante);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                return StatusCode(500, new { error = "Error al guardar el Votante.", details = innerException?.Message });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { error = "Error al guardar el Votante.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar el Votante.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var votante = await context.Simpatizantes.FindAsync(id);

            if (votante == null)
            {
                return NotFound();
            }

            context.Simpatizantes.Remove(votante);
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

            var Votantes = await context.Simpatizantes.FindAsync(id);

            if (Votantes == null)
            {
                return NotFound();
            }

            mapper.Map(dto, Votantes);
            Votantes.Seccion = await context.Secciones.SingleOrDefaultAsync(i => i.Id == dto.Seccion.Id);
            Votantes.Municipio = await context.Municipios.SingleOrDefaultAsync(i => i.Id == dto.Municipio.Id);
            Votantes.Estado = await context.Estados.SingleOrDefaultAsync(c => c.Id == dto.Estado.Id);
            Votantes.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(c => c.Id == dto.ProgramaSocial.Id);
            context.Update(Votantes);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidenciasExists(id))
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

        private bool IncidenciasExists(int id)
        {
            return context.Incidencias.Any(e => e.Id == id);
        }

    }
}