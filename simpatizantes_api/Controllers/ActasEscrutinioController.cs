using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace simpatizantes_api.Controllers
{
    [Route("api/actas")]
    [ApiController]
    public class ActasEscrutinioController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActasEscrutinioController (ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<ActaEscrutinioDTO>> GetById(int id)
        {
            var actaEscrutinio = await context.ActasEscrutinios
                .Include(u => u.Distrito)
                .Include(u => u.Municipio)
                .Include(u => u.Seccion)
                .Include(u => u.Casilla)
                .Include(u => u.TipoEleccion)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (actaEscrutinio == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ActaEscrutinioDTO>(actaEscrutinio));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ActaEscrutinioDTO>>> GetAll()
        {
            var actaEscrutinio = await context.ActasEscrutinios
                .Include(u => u.Distrito)
                .Include(u => u.Municipio)
                .Include(u => u.Seccion)
                .Include(u => u.Casilla)
                .Include(u => u.TipoEleccion)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!actaEscrutinio.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ActaEscrutinioDTO>>(actaEscrutinio));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(ActaEscrutinioDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var actaEscrutinio = mapper.Map<ActaEscrutinio>(dto);
                actaEscrutinio.Distrito = await context.Distritos.SingleOrDefaultAsync(r => r.Id == dto.Distrito.Id);
                actaEscrutinio.Municipio = await context.Municipios.SingleOrDefaultAsync(r => r.Id == dto.Municipio.Id);
                actaEscrutinio.Seccion = await context.Secciones.SingleOrDefaultAsync(r => r.Id == dto.Seccion.Id);
                actaEscrutinio.Casilla = await context.Casillas.SingleOrDefaultAsync(r => r.Id == dto.Casilla.Id);
                actaEscrutinio.TipoEleccion = await context.TiposElecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);

                context.Add(actaEscrutinio);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar al usuario.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actaEscrutinio = await context.ActasEscrutinios.FindAsync(id);

            if (actaEscrutinio == null)
            {
                return NotFound();
            }

            context.ActasEscrutinios.Remove(actaEscrutinio);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActaEscrutinioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var actaEscrutinio = await context.ActasEscrutinios.FindAsync(id);
            
            if (actaEscrutinio == null)
            {
                return NotFound();
            }

            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, actaEscrutinio);
            actaEscrutinio.Distrito = await context.Distritos.SingleOrDefaultAsync(r => r.Id == dto.Distrito.Id);
            actaEscrutinio.Municipio = await context.Municipios.SingleOrDefaultAsync(r => r.Id == dto.Municipio.Id);
            actaEscrutinio.Seccion = await context.Secciones.SingleOrDefaultAsync(r => r.Id == dto.Seccion.Id);
            actaEscrutinio.Casilla = await context.Casillas.SingleOrDefaultAsync(r => r.Id == dto.Casilla.Id);
            actaEscrutinio.TipoEleccion = await context.TiposElecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);

            context.Update(actaEscrutinio);

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
            return context.ActasEscrutinios.Any(e => e.Id == id);
        }

    }
}