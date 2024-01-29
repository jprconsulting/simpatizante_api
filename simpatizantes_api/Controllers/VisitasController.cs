using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace simpatizantes_api.Controllers
{
    [Route("api/visitas")]
    [ApiController]
    public class VisitasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public VisitasController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<VisitaDTO>> GetById(int id)
        {
            var visita = await context.Visitas
                .Include(b => b.Simpatizante)
                .Include(o => o.Operador)
                .Include(c => c.Candidato)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (visita == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VisitaDTO>(visita));
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
                var visitas = await context.Visitas
                .Include(v => v.Simpatizante)
                .ThenInclude(b => b.Municipio)
                .Include(o => o.Operador)
                .Include(c => c.Candidato)
                .ToListAsync();

                if (!visitas.Any())
                {
                    return NotFound();
                }

                var visitasDTO = mapper.Map<List<VisitaDTO>>(visitas);

                foreach (var visita in visitasDTO)
                {
                    visita.ImagenBase64 = GetBase64Image(visita.Foto); // Asigna el base64 de la imagen
                }

                return Ok(visitasDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }



        [HttpPost("crear")]
        public async Task<ActionResult> Post(VisitaDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                byte[] bytes = Convert.FromBase64String(dto.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Foto = fileName;
            }

            var visita = mapper.Map<Visita>(dto);
            visita.FechaHoraVisita = DateTime.Now;
            if (dto.Simpatizante != null)
            {
                visita.Simpatizante = await context.Simpatizantes.SingleOrDefaultAsync(c => c.Id == dto.Simpatizante.Id);
            }
            if (dto.Operador != null)
            {
                visita.Operador = await context.Operadores.SingleOrDefaultAsync(c => c.Id == dto.Operador.Id);
            }
            if (dto.Candidato != null)
            {
                visita.Candidato = await context.Candidatos.SingleOrDefaultAsync(c => c.Id == dto.Candidato.Id);
            }
           

            context.Visitas.Add(visita);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, VisitaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var visita = await context.Visitas.FindAsync(id);
            if (dto.Simpatizante != null)
            {
                visita.Simpatizante = await context.Simpatizantes.SingleOrDefaultAsync(c => c.Id == dto.Simpatizante.Id);
            }
            if (dto.Operador != null)
            {
                visita.Operador = await context.Operadores.SingleOrDefaultAsync(c => c.Id == dto.Operador.Id);
            }
            if (dto.Candidato != null)
            {
                visita.Candidato = await context.Candidatos.SingleOrDefaultAsync(c => c.Id == dto.Candidato.Id);
            }

            if (visita == null)
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

            mapper.Map(dto, visita);
            visita.Simpatizante   = await context.Simpatizantes.SingleOrDefaultAsync(b => b.Id == dto.Simpatizante.Id);

            context.Update(visita);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Visitas.Any(e => e.Id == id))
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
            var visita = await context.Visitas.FindAsync(id);

            if (visita == null)
            {
                return NotFound();
            }

            context.Visitas.Remove(visita);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}