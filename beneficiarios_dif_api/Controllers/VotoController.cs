using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/voto")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public VotoController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<VotoDTO>> GetById(int id)
        {
            var voto = await context.Votos
                .Include(b => b.Votante)                
                .FirstOrDefaultAsync(v => v.Id == id);

            if (voto == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VotoDTO>(voto));
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
                var votos = await context.Votos
                .Include(v => v.Votante)
                .ThenInclude(b => b.Municipio)                
                .ToListAsync();

                if (!votos.Any())
                {
                    return NotFound();
                }

                var votoDTO = mapper.Map<List<VotoDTO>>(votos);

                foreach (var voto in votoDTO)
                {
                    voto.ImagenBase64 = GetBase64Image(voto.Foto); // Asigna el base64 de la imagen
                }

                return Ok(votoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }



        [HttpPost("crear")]
        public async Task<ActionResult> Post(VotoDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                byte[] bytes = Convert.FromBase64String(dto.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Foto = fileName;
            }

            var voto = mapper.Map<Voto>(dto);
            voto.Votante = await context.Votantes.SingleOrDefaultAsync(b => b.Id == dto.Votante.Id);

            context.Votos.Add(voto);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, VotoDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var voto = await context.Votos.FindAsync(id);

            if (voto == null)
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

            mapper.Map(dto, voto);
            voto.Votante = await context.Votantes.SingleOrDefaultAsync(b => b.Id == dto.Votante.Id);

            context.Update(voto);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Votos.Any(e => e.Id == id))
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
            var voto = await context.Votos.FindAsync(id);

            if (voto == null)
            {
                return NotFound();
            }

            context.Votos.Remove(voto);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}