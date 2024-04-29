using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Services;
using simpatizantes_api.Filters;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/incidencias")]
    [ApiController]
    [TokenValidationFilter]

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
            var incidencia = await context.incidencias
                .Include(t => t.TipoIncidencia)
                .Include(c => c.Casilla)
                .Include(c => c.Candidato)
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
                var incidencias = await context.incidencias
                    .Include(t => t.TipoIncidencia)
                    .Include(c => c.Casilla)
                    .Include(c => c.Candidato)
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

        [HttpGet("obtener-incidencias-por-tipo-id/{tipoIncidenciaId:int}")]
        public async Task<ActionResult<List<IncidenciaDTO>>> ObtenerIncidenciasPorTipoId(int tipoIncidenciaId)

        {
            var incidencias = await context.incidencias
                .Include(t => t.TipoIncidencia)
                .Include(c => c.Casilla)
                .Include(c => c.Candidato)
                .Where(s => s.TipoIncidencia.Id == tipoIncidenciaId)
                .ToListAsync();

            if (!incidencias.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<IncidenciaDTO>>(incidencias));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(IncidenciaDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioIncidencias);
            }
            var existeIncidencia = await context.incidencias.AnyAsync(n => n.Retroalimentacion == dto.Retroalimentacion);
            if (existeIncidencia)
            {
                return Conflict();
            }

            var incidencia = mapper.Map<Incidencia>(dto);
            incidencia.TipoIncidencia = await context.tiposincidencias.SingleOrDefaultAsync(b => b.Id == dto.TipoIncidencia.Id);
            incidencia.Casilla = await context.casillas.SingleOrDefaultAsync(o => o.Id == dto.Casilla.Id);
            incidencia.Candidato = await context.candidatos.SingleOrDefaultAsync(o => o.Id == dto.Candidato.Id);

            context.incidencias.Add(incidencia);
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

            var incidencia = await context.incidencias.FindAsync(id);

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
            incidencia.TipoIncidencia = await context.tiposincidencias.SingleOrDefaultAsync(b => b.Id == dto.TipoIncidencia.Id);
            incidencia.Casilla = await context.casillas.SingleOrDefaultAsync(o => o.Id == dto.Casilla.Id);
            incidencia.Candidato = await context.candidatos.SingleOrDefaultAsync(o => o.Id == dto.Candidato.Id);

            context.Update(incidencia);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.incidencias.Any(e => e.Id == id))
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
            var incidencia = await context.incidencias.FindAsync(id);

            if (incidencia == null)
            {
                return NotFound();
            }

            context.incidencias.Remove(incidencia);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}