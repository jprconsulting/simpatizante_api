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
    [Route("api/tipos-agrupaciones")]
    [ApiController]
    [TokenValidationFilter]

    public class TiposAgrupacionesPoliticasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TiposAgrupacionesPoliticasController (ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<TipoAgrupacionPoliticaDTO>> GetById(int id)
        {
            var tipoAgrupaciones = await context.tiposagrupacionespoliticas
                .FirstOrDefaultAsync(u => u.Id == id);

            if (tipoAgrupaciones == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TipoAgrupacionPoliticaDTO>(tipoAgrupaciones));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<TipoAgrupacionPoliticaDTO>>> GetAll()
        {
            var tipoAgrupaciones = await context.tiposagrupacionespoliticas
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!tipoAgrupaciones.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<TipoAgrupacionPoliticaDTO>>(tipoAgrupaciones));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(TipoAgrupacionPoliticaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var tipoAgrupaciones = mapper.Map<TipoAgrupacionPolitica>(dto);

                context.Add(tipoAgrupaciones);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar el tipo de agrupacion.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipoAgrupaciones = await context.tiposagrupacionespoliticas.FindAsync(id);

            if (tipoAgrupaciones == null)
            {
                return NotFound();
            }

            context.tiposagrupacionespoliticas.Remove(tipoAgrupaciones);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoEleccionDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var tipoAgrupaciones = await context.tiposagrupacionespoliticas.FindAsync(id);

            if (tipoAgrupaciones == null)
            {
                return NotFound();
            }

            mapper.Map(dto, tipoAgrupaciones);

            context.Update(tipoAgrupaciones);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoExists(id))
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

        private bool TipoExists(int id)
        {
            return context.tiposagrupacionespoliticas.Any(e => e.Id == id);
        }

    }
}