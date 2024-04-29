using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using simpatizantes_api.Filters;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/resultados")]
    [ApiController]
    [TokenValidationFilter]

    public class ResultadosCandidaturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ResultadosCandidaturasController (ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<ResultadoCandidaturaDTO>> GetById(int id)
        {
            var resultadoCandidatura = await context.resultadoscandidaturas
                .Include(u => u.ActaEscrutinio)
                .Include(u => u.DistribucionCandidatura)
                .Include(u => u.Candidatura)
                .Include(u => u.Combinacion)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (resultadoCandidatura == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ResultadoCandidaturaDTO>(resultadoCandidatura));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ResultadoCandidaturaDTO>>> GetAll()
        {
            var resultadoCandidatura = await context.resultadoscandidaturas
                .Include(u => u.ActaEscrutinio)
                .Include(u => u.DistribucionCandidatura)
                .Include(u => u.Candidatura)
                .Include(u => u.Combinacion)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!resultadoCandidatura.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ResultadoCandidaturaDTO>>(resultadoCandidatura));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(ResultadoCandidaturaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var resultadoCandidatura = mapper.Map<ResultadoCandidatura>(dto);
                resultadoCandidatura.ActaEscrutinio = await context.actasescrutinios.SingleOrDefaultAsync(r => r.Id == dto.ActaEscrutinio.Id);
                resultadoCandidatura.DistribucionCandidatura = await context.distribucionescandidaturas.SingleOrDefaultAsync(r => r.Id == dto.DistribucionCandidatura.Id);
                resultadoCandidatura.Candidatura = await context.candidaturas.SingleOrDefaultAsync(r => r.Id == dto.Candidatura.Id);
                resultadoCandidatura.Combinacion = await context.combinaciones.SingleOrDefaultAsync(r => r.Id == dto.Combinacion.Id);

                context.Add(resultadoCandidatura);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar el resultado.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultadoCandidatura = await context.resultadoscandidaturas.FindAsync(id);

            if (resultadoCandidatura == null)
            {
                return NotFound();
            }

            context.resultadoscandidaturas.Remove(resultadoCandidatura);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ResultadoCandidaturaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var resultadoCandidatura = await context.resultadoscandidaturas.FindAsync(id);

            if (resultadoCandidatura == null)
            {
                return NotFound();
            }

            mapper.Map(dto, resultadoCandidatura);
            resultadoCandidatura.ActaEscrutinio = await context.actasescrutinios.SingleOrDefaultAsync(r => r.Id == dto.ActaEscrutinio.Id);
            resultadoCandidatura.DistribucionCandidatura = await context.distribucionescandidaturas.SingleOrDefaultAsync(r => r.Id == dto.DistribucionCandidatura.Id);
            resultadoCandidatura.Candidatura = await context.candidaturas.SingleOrDefaultAsync(r => r.Id == dto.Candidatura.Id);
            resultadoCandidatura.Combinacion = await context.combinaciones.SingleOrDefaultAsync(r => r.Id == dto.Combinacion.Id);
            context.Update(resultadoCandidatura);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultadosExists(id))
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

        private bool ResultadosExists(int id)
        {
            return context.resultadoscandidaturas.Any(e => e.Id == id);
        }

    }
}