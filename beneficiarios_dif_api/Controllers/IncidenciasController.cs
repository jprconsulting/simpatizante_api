using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/incidencias")]
    [ApiController]
    public class IncidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public IncidenciasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<IncidenciaDTO>> GetById(int id)
        {
            var incidencia = await context.Incidencias

                .Include(i => i.TipoIncidencia)
                .Include(c => c.Casilla)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (incidencia == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IncidenciaDTO>(incidencia));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<IncidenciaDTO>>> GetAll()
        {
            var incidencia = await context.Incidencias
                .Include(i => i.TipoIncidencia)
                .Include(c => c.Casilla)
                .ToListAsync();

            if (!incidencia.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<IncidenciaDTO>>(incidencia));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(IncidenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var incidencia = mapper.Map<Incidencia>(dto);
            incidencia.TipoIncidencia = await context.Indicadores.SingleOrDefaultAsync(i => i.Id == dto.Indicador.Id);
            incidencia.Casilla = await context.Casillas.SingleOrDefaultAsync(c => c.Id == dto.Casilla.Id);

            context.Add(incidencia);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar la incidencia.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var incidencia = await context.Incidencias.FindAsync(id);

            if (incidencia == null)
            {
                return NotFound();
            }

            context.Incidencias.Remove(incidencia);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, IncidenciaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var incidencia = await context.Incidencias.FindAsync(id);

            if (incidencia == null)
            {
                return NotFound();
            }

            mapper.Map(dto, incidencia);
            incidencia.TipoIncidencia = await context.Indicadores.SingleOrDefaultAsync(i => i.Id == dto.Indicador.Id);
            incidencia.Casilla = await context.Casillas.SingleOrDefaultAsync(c => c.Id == dto.Casilla.Id);
            context.Update(incidencia);

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