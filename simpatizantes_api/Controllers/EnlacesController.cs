using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/enlaces")]
    [ApiController]
    public class EnlacesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EnlacesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<EnlaceDTO>>> GetAll()
        {
            var enlaces = await context.Enlaces.ToListAsync();

            if (!enlaces.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<EnlaceDTO>>(enlaces));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(EnlaceDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeEnlace = await context.Enlaces.AnyAsync(n => n.Nombres == dto.Nombres &&
                                                                   n.ApellidoPaterno == dto.ApellidoPaterno &&
                                                                   n.ApellidoMaterno == dto.ApellidoMaterno);

            if (existeEnlace)
            {
                return Conflict();
            }

            var enlace = mapper.Map<Enlace>(dto);
            context.Add(enlace);

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
            var enlace = await context.Enlaces.FindAsync(id);

            if (enlace == null)
            {
                return NotFound();
            }

            var tieneDependencias = await context.Simpatizantes.AnyAsync(s => s.Enlace.Id == id);

            if (tieneDependencias)
            {
                return StatusCode(502, new { error = "No se puede eliminar el enlace debido a dependencias existentes." });
            }

            context.Enlaces.Remove(enlace);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] EnlaceDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var enlace = await context.Enlaces.FindAsync(id);

            if (enlace == null)
            {
                return NotFound();
            }

            mapper.Map(dto, enlace);
            context.Update(enlace);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnlaceExists(id))
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

        private bool EnlaceExists(int id)
        {
            return context.Enlaces.Any(e => e.Id == id);
        }
    }
}
