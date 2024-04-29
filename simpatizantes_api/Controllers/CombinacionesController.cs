using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using simpatizantes_api.Services;
using simpatizantes_api.Filters;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/combinaciones")]
    [ApiController]
    [TokenValidationFilter]

    public class CombinacionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioCombinaciones = "combinaciones";
        private readonly IMapper mapper;

        public CombinacionesController(
            ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<CombinacionDTO>> GetById(int id)
        {
            var combinacion = await context.combinaciones
                .Include(u => u.Candidatura)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (combinacion == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CombinacionDTO>(combinacion));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<CombinacionDTO>>> GetAll()
        {
            var combinacion = await context.combinaciones
                .Include(u => u.Candidatura)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!combinacion.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CombinacionDTO>>(combinacion));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(CombinacionDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Logo = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioCombinaciones);
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var combinacion = mapper.Map<Combinacion>(dto);
                combinacion.Candidatura = await context.candidaturas.SingleOrDefaultAsync(r => r.Id == dto.Candidatura.Id);
                if (dto.Partidos == null || dto.Partidos.Count == 0)
                {
                    return BadRequest("Debe proporcionar al menos un partido para el tipo de agrupación política seleccionado.");
                }

                // Convierte los objetos CandidaturaDTO a entidades Candidatura y añádelos a la lista de Partidos en la entidad Candidatura
                combinacion.Partidos = string.Join(",", dto.Partidos);

                context.Add(combinacion);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar la combinacion.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var combinacion = await context.combinaciones.FindAsync(id);

            if (combinacion == null)
            {
                return NotFound();
            }

            context.combinaciones.Remove(combinacion);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CombinacionDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var combinacion = await context.combinaciones.FindAsync(id);

            if (combinacion == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {

                dto.Logo = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioCombinaciones);
                combinacion.Partidos = string.Join(",", dto.Partidos);
            }
            else
            {
                dto.Logo = combinacion.Logo;
                combinacion.Partidos = string.Join(",", dto.Partidos);
            }

            
            mapper.Map(dto, combinacion);
            combinacion.Candidatura = await context.candidaturas.SingleOrDefaultAsync(r => r.Id == dto.Candidatura.Id);

            context.Update(combinacion);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CombinacionesExists(id))
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

        private bool CombinacionesExists(int id)
        {
            return context.combinaciones.Any(e => e.Id == id);
        }

    }
}