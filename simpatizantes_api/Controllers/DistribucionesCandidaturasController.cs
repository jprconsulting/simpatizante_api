using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace simpatizantes_api.Controllers
{
    [Route("api/distribucion-candidatura")]
    [ApiController]
    public class DistribucionesCandidaturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DistribucionesCandidaturasController (ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<DistribucionCandidaturaDTO>> GetById(int id)
        {
            var distribucionCandidatura = await context.DistribucionesCandidaturas
                .Include(u => u.TipoEleccion)
                .Include(u => u.Distrito)
                .Include(u => u.Municipio)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (distribucionCandidatura == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DistribucionCandidaturaDTO>(distribucionCandidatura));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<DistribucionCandidaturaDTO>>> GetAll()
        {
            var distribucionCandidatura = await context.DistribucionesCandidaturas
                .Include(u => u.TipoEleccion)
                .Include(u => u.Distrito)
                .Include(u => u.Municipio)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!distribucionCandidatura.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<DistribucionCandidaturaDTO>>(distribucionCandidatura));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(DistribucionCandidaturaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var distribucionCandidatura = mapper.Map<DistribucionCandidatura>(dto);
                distribucionCandidatura.TipoEleccion = await context.TiposElecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);
                distribucionCandidatura.Distrito = await context.Distritos.SingleOrDefaultAsync(r => r.Id == dto.Distrito.Id);
                distribucionCandidatura.Municipio = await context.Municipios.SingleOrDefaultAsync(r => r.Id == dto.Municipio.Id);

                context.Add(distribucionCandidatura);
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
            var distribucionCandidatura = await context.DistribucionesCandidaturas.FindAsync(id);

            if (distribucionCandidatura == null)
            {
                return NotFound();
            }

            context.DistribucionesCandidaturas.Remove(distribucionCandidatura);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] DistribucionCandidaturaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var distribucionCandidatura = await context.DistribucionesCandidaturas.FindAsync(id);

            if (distribucionCandidatura == null)
            {
                return NotFound();
            }

            mapper.Map(dto, distribucionCandidatura);
            distribucionCandidatura.TipoEleccion = await context.TiposElecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);
            distribucionCandidatura.Distrito = await context.Distritos.SingleOrDefaultAsync(r => r.Id == dto.Distrito.Id);
            distribucionCandidatura.Municipio = await context.Municipios.SingleOrDefaultAsync(r => r.Id == dto.Municipio.Id);
            context.Update(distribucionCandidatura);

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
            return context.DistribucionesCandidaturas.Any(e => e.Id == id);
        }

    }
}