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
    [Route("api/tipos-elecciones")]
    [ApiController]
    [TokenValidationFilter]

    public class TiposEleccionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TiposEleccionesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<TipoEleccionDTO>> GetById(int id)
        {
            var TipoElecciones = await context.TiposElecciones               
                .FirstOrDefaultAsync(u => u.Id == id);

            if (TipoElecciones == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TipoEleccionDTO>(TipoElecciones));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<TipoEleccionDTO>>> GetAll()
        {
            var tipoElecciones = await context.TiposElecciones
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!tipoElecciones.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<TipoEleccionDTO>>(tipoElecciones));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(TipoEleccionDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var tipoElecciones = mapper.Map<TipoEleccion>(dto);               

                context.Add(tipoElecciones);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar el tipo de eleccion.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tipoElecciones = await context.TiposElecciones.FindAsync(id);

            if (tipoElecciones == null)
            {
                return NotFound();
            }

            context.TiposElecciones.Remove(tipoElecciones);
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

            var tipoElecciones = await context.TiposElecciones.FindAsync(id);

            if (tipoElecciones == null)
            {
                return NotFound();
            }

            mapper.Map(dto, tipoElecciones);
            
            context.Update(tipoElecciones);

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
            return context.TiposElecciones.Any(e => e.Id == id);
        }

    }
}