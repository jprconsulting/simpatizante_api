using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace simpatizantes_api.Controllers
{
    [Route("api/tipos-incidencias")]
    [ApiController]
    public class TiposIncidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TiposIncidenciasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<TipoIncidenciaDTO>>> GetAll()
        {
            var tiposIncidencias = await context.TiposIncidencias.ToListAsync();

            if (!tiposIncidencias.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<TipoIncidenciaDTO>>(tiposIncidencias));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(TipoIncidenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeTipoIncidencia = await context.TiposIncidencias.AnyAsync(n => n.Tipo == dto.Tipo);

            if (existeTipoIncidencia)
            {
                return Conflict();
            }

            var tipoIncidencia = mapper.Map<TipoIncidenciaDTO>(dto);
            context.Add(tipoIncidencia);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipoIncidencia = await context.TiposIncidencias.FindAsync(id);

            if (tipoIncidencia == null)
            {
                return NotFound();
            }

            context.TiposIncidencias.Remove(tipoIncidencia);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoIncidenciaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var tipoIncidencia = await context.TiposIncidencias.FindAsync(id);

            if (tipoIncidencia == null)
            {
                return NotFound();
            }

            mapper.Map(dto, tipoIncidencia);
            context.Update(tipoIncidencia);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoIncidenciaExists(id))
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

        private bool TipoIncidenciaExists(int id)
        {
            return context.TiposIncidencias.Any(e => e.Id == id);
        }
    }
}
