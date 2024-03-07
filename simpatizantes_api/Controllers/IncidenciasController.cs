using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Services;
using simpatizantesapi.Migrations;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/incidencias")]
    [ApiController]
    public class IncidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioIncidencias = "incidencias";

        public IncidenciasController(
            ApplicationDbContext context,
            IMapper mapper, 
            IWebHostEnvironment webHostEnvironment,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<IncidenciaDTO>> GetById(int id)
        {
            var incidencia = await context.Incidencias
                .Include(t => t.TipoIncidencia)
                .Include(c => c.Casilla)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (incidencia == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IncidenciaDTO>(incidencia));
        }     

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<IncidenciaDTO>>> GetAll()
        {
            try
            {
                var incidencias = await context.Incidencias
                    .Include(t => t.TipoIncidencia)
                    .Include(c => c.Casilla)
                    .ToListAsync();

                if (!incidencias.Any())
                {
                    return NotFound();
                }

                return Ok(mapper.Map<List<IncidenciaDTO>>(incidencias));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }



        [HttpPost("crear")]
        public async Task<ActionResult> Post(IncidenciaDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioIncidencias);
            }
            var existeIncidencia = await context.Incidencias.AnyAsync(n => n.Retroalimentacion == dto.Retroalimentacion);
            if (existeIncidencia)
            {
                return Conflict();
            }

            var incidencia = mapper.Map<Incidencia>(dto);
            incidencia.TipoIncidencia = await context.TiposIncidencias.SingleOrDefaultAsync(b => b.Id == dto.TipoIncidencia.Id);
            incidencia.Casilla = await context.Casillas.SingleOrDefaultAsync(o => o.Id == dto.Casilla.Id);

            context.Incidencias.Add(incidencia);
            await context.SaveChangesAsync();

            return Ok();
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

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioIncidencias);
            }
            else
            {
                dto.Foto = incidencia.Foto;
            }

            mapper.Map(dto, incidencia);
            incidencia.TipoIncidencia = await context.TiposIncidencias.SingleOrDefaultAsync(b => b.Id == dto.TipoIncidencia.Id);
            incidencia.Casilla = await context.Casillas.SingleOrDefaultAsync(o => o.Id == dto.Casilla.Id);

            context.Update(incidencia);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Incidencias.Any(e => e.Id == id))
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

    }
}