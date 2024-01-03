using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Authorize]
    [Route("api/areas-adscripcion")]
    [ApiController]
    public class AreasAdscripcionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AreasAdscripcionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<AreaAdscripcionDTO>> GetById(int id)
        {
            var areaAdscripcion = await context.AreasAdscripcion.FirstOrDefaultAsync(a => a.Id == id);

            if (areaAdscripcion == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AreaAdscripcionDTO>(areaAdscripcion));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<AreaAdscripcionDTO>>> GetAll()
        {
            var areasAdscripcion = await context.AreasAdscripcion.ToListAsync();

            if (!areasAdscripcion.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<AreaAdscripcionDTO>>(areasAdscripcion));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(AreaAdscripcionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeAreaAdscripcion = await context.AreasAdscripcion.AnyAsync(a => a.Nombre == dto.Nombre);

            if (existeAreaAdscripcion)
            {
                return Conflict();
            }

            context.Add(mapper.Map<AreaAdscripcion>(dto));

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
            var areaAdscripcion = await context.AreasAdscripcion.FindAsync(id);

            if (areaAdscripcion == null)
            {
                return NotFound();
            }

            context.AreasAdscripcion.Remove(areaAdscripcion);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, AreaAdscripcionDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var areaAdscripcion = await context.AreasAdscripcion.FindAsync(id);

            if (areaAdscripcion == null)
            {
                return NotFound();
            }

            mapper.Map(dto, areaAdscripcion);
            context.Update(areaAdscripcion);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaAdscripcionExists(id))
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

        private bool AreaAdscripcionExists(int id)
        {
            return context.AreasAdscripcion.Any(e => e.Id == id);
        }
    }
}
