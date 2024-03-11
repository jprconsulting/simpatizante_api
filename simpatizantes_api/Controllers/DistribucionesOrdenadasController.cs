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
    [Route("api/distribucion-ordenada")]
    [ApiController]
    public class DistribucionesOrdenadasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioDistribuciones = "distribuciones";

        public DistribucionesOrdenadasController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<DistribucionOrdenadaDTO>> GetById(int id)
        {
            var distribucionOrdenada = await context.DistribucionesOrdenadas
                .Include(u => u.DistribucionCandidatura)
                .Include(u => u.TipoAgrupacionPolitica)
                .Include(u => u.Candidatura)
                .Include(u => u.Combinacion)

                .FirstOrDefaultAsync(u => u.Id == id);

            if (distribucionOrdenada == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DistribucionOrdenadaDTO>(distribucionOrdenada));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<DistribucionOrdenadaDTO>>> GetAll()
        {
            var distribucionOrdenada = await context.DistribucionesOrdenadas
                .Include(u => u.DistribucionCandidatura)
                .Include(u => u.TipoAgrupacionPolitica)
                .Include(u => u.Candidatura)
                .Include(u => u.Combinacion)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!distribucionOrdenada.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<DistribucionOrdenadaDTO>>(distribucionOrdenada));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(DistribucionOrdenadaDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Logo = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioDistribuciones);
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var distribucionOrdenada = mapper.Map<DistribucionOrdenada>(dto);
                distribucionOrdenada.DistribucionCandidatura = await context.DistribucionesCandidaturas.SingleOrDefaultAsync(r => r.Id == dto.DistribucionCandidatura.Id);
                distribucionOrdenada.TipoAgrupacionPolitica = await context.TiposAgrupacionesPoliticas.SingleOrDefaultAsync(r => r.Id == dto.TipoAgrupacionPolitica.Id);
                distribucionOrdenada.Candidatura = await context.Candidaturas.SingleOrDefaultAsync(r => r.Id == dto.Candidatura.Id);
                distribucionOrdenada.Combinacion = await context.Combinaciones.SingleOrDefaultAsync(r => r.Id == dto.Combinacion.Id);

                context.Add(distribucionOrdenada);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar la distribucion.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var distribucionOrdenada = await context.DistribucionesOrdenadas.FindAsync(id);

            if (distribucionOrdenada == null)
            {
                return NotFound();
            }

            context.DistribucionesOrdenadas.Remove(distribucionOrdenada);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] DistribucionOrdenadaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var distribucionOrdenada = await context.DistribucionesOrdenadas.FindAsync(id);

            if (distribucionOrdenada == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Logo = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioDistribuciones);
            }

            mapper.Map(dto, distribucionOrdenada);
            distribucionOrdenada.DistribucionCandidatura = await context.DistribucionesCandidaturas.SingleOrDefaultAsync(r => r.Id == dto.DistribucionCandidatura.Id);
            distribucionOrdenada.TipoAgrupacionPolitica = await context.TiposAgrupacionesPoliticas.SingleOrDefaultAsync(r => r.Id == dto.TipoAgrupacionPolitica.Id);
            distribucionOrdenada.Candidatura = await context.Candidaturas.SingleOrDefaultAsync(r => r.Id == dto.Candidatura.Id);
            distribucionOrdenada.Combinacion = await context.Combinaciones.SingleOrDefaultAsync(r => r.Id == dto.Combinacion.Id);
            context.Update(distribucionOrdenada);

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
            return context.DistribucionesOrdenadas.Any(e => e.Id == id);
        }

    }
}