using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using simpatizantes_api.Services;

namespace simpatizantes_api.Controllers
{
    [Route("api/candidaturas")]
    [ApiController]
    public class CandidaturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioCandidaturas = "candidaturas";

        public CandidaturasController (
            ApplicationDbContext context, 
            IMapper mapper,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<CandidaturaDTO>> GetById(int id)
        {
            var candidatura = await context.Candidaturas
                .Include(u => u.TipoAgrupacionPolitica)                
                .FirstOrDefaultAsync(u => u.Id == id);

            if (candidatura == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CandidaturaDTO>(candidatura));
        }

        [HttpGet("obtener-por-tipo-agrupacion-partido")]
        public async Task<ActionResult<List<CandidaturaDTO>>> GetByTipoAgrupacion()
        {
            var candidaturas = await context.Candidaturas
                .Include(u => u.TipoAgrupacionPolitica)
                .Where(c => c.TipoAgrupacionPolitica.Id == 1)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (!candidaturas.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CandidaturaDTO>>(candidaturas));
        }

        [HttpGet("obtener-por-tipo-coalicion")]
        public async Task<ActionResult<List<CandidaturaDTO>>> GetByTipoCoalicion()
        {
            var candidaturas = await context.Candidaturas
                .Include(u => u.TipoAgrupacionPolitica)
                .Where(c => c.TipoAgrupacionPolitica.Id == 3)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (!candidaturas.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CandidaturaDTO>>(candidaturas));
        }

        [HttpGet("obtener-por-tipo-independiente")]
        public async Task<ActionResult<List<CandidaturaDTO>>> GetByTipoIndependiente()
        {
            var candidaturas = await context.Candidaturas
                .Include(u => u.TipoAgrupacionPolitica)
                .Where(c => c.TipoAgrupacionPolitica.Id == 4)
                .OrderBy(c => c.Id)
                .ToListAsync();

            if (!candidaturas.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CandidaturaDTO>>(candidaturas));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<CandidaturaDTO>>> GetAll()
        {
            var candidatura = await context.Candidaturas
                .Include(u => u.TipoAgrupacionPolitica)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!candidatura.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CandidaturaDTO>>(candidatura));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(CandidaturaDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Logo = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioCandidaturas);
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var candidatura = mapper.Map<Candidatura>(dto);
                candidatura.TipoAgrupacionPolitica = await context.TiposAgrupacionesPoliticas.SingleOrDefaultAsync(r => r.Id == dto.TipoAgrupacionPolitica.Id);

                // Verifica si el tipo de agrupación política es 1, 3 O 4 (Partido Político, Candidatura Común o Candidatura Independiente)
                if (candidatura.TipoAgrupacionPolitica.Id == 1 || candidatura.TipoAgrupacionPolitica.Id == 3 || candidatura.TipoAgrupacionPolitica.Id == 4)
                {
                    // No se requiere realizar ninguna acción adicional para estos tipos de agrupación
                }
                else
                {
                    // Si el tipo de agrupación política es 3, verificamos si se han proporcionado partidos
                    if (dto.Partidos == null || dto.Partidos.Count == 0)
                    {
                        return BadRequest("Debe proporcionar al menos un partido para el tipo de agrupación política seleccionado.");
                    }

                    // Convierte los objetos CandidaturaDTO a entidades Candidatura y añádelos a la lista de Partidos en la entidad Candidatura
                    candidatura.Partidos = string.Join(",", dto.Partidos);
                }

                context.Add(candidatura);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar la candidatura.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var candidatura = await context.Candidaturas.FindAsync(id);

            if (candidatura == null)
            {
                return NotFound();
            }

            context.Candidaturas.Remove(candidatura);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CandidaturaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var candidatura = await context.Candidaturas.FindAsync(id);

            if (candidatura == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {

                dto.Logo = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioCandidaturas);
            }
            else
            {
                dto.Logo = candidatura.Logo;
            }
            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, candidatura);
            candidatura.TipoAgrupacionPolitica = await context.TiposAgrupacionesPoliticas.SingleOrDefaultAsync(r => r.Id == dto.TipoAgrupacionPolitica.Id);

            context.Update(candidatura);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActaExists(id))
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

        private bool ActaExists(int id)
        {
            return context.Candidaturas.Any(e => e.Id == id);
        }

    }
}