using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public IncidenciasController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
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

        private string GetBase64Image(string fileName)
        {
            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);

            if (System.IO.File.Exists(filePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }

            return null;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult> GetAll()
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

                var incidenciasDTO = mapper.Map<List<IncidenciaDTO>>(incidencias);

                foreach (var incidencia in incidenciasDTO)
                {
                    incidencia.ImagenBase64 = GetBase64Image(incidencia.Foto); // Asigna el base64 de la imagen
                }

                return Ok(incidenciasDTO);
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
                byte[] bytes = Convert.FromBase64String(dto.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Foto = fileName;
            }

            var incidencia = mapper.Map<Incidencia>(dto);
            incidencia.TipoIncidencia = await context.Indicadores.SingleOrDefaultAsync(b => b.Id == dto.TipoIncidencia.Id);
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
                byte[] bytes = Convert.FromBase64String(dto.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Foto = fileName;
            }

            mapper.Map(dto, incidencia);
            incidencia.TipoIncidencia = await context.Indicadores.SingleOrDefaultAsync(b => b.Id == dto.TipoIncidencia.Id);
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