using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/candidato")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CandidatoController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<CandidatoDTO>> GetById(int id)
        {
            var votante = await context.Candidatos
                .Include(c => c.Cargo)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (votante == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CandidatoDTO>(votante));
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
                var incidencias = await context.Candidatos
                    .Include(t => t.Cargo)
                    .ToListAsync();

                if (!incidencias.Any())
                {
                    return NotFound();
                }

                var incidenciasDTO = mapper.Map<List<CandidatoDTO>>(incidencias);

                foreach (var incidencia in incidenciasDTO)
                {
                    incidencia.ImagenBase64 = GetBase64Image(incidencia.Foto); // Asigna el base64 de la imagen
                }

                foreach (var incidencia in incidenciasDTO)
                {
                    incidencia.EmblemaBase64 = GetBase64Image(incidencia.Emblema); // Asigna el base64 de la imagen
                }
                return Ok(incidenciasDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(CandidatoDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                byte[] bytes = Convert.FromBase64String(dto.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Foto = fileName;
            }
            if (!string.IsNullOrEmpty(dto.EmblemaBase64))
            {
                byte[] bytes = Convert.FromBase64String(dto.EmblemaBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Emblema = fileName;
            }
            var candidato = mapper.Map<Candidato>(dto);
            candidato.Cargo = await context.Cargos.SingleOrDefaultAsync(b => b.Id == dto.Cargo.Id);

            context.Candidatos.Add(candidato);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var candidato = await context.Candidatos.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }

            context.Candidatos.Remove(candidato);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, CandidatoDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var Candidatos = await context.Candidatos.FindAsync(id);

            if (Candidatos == null)
            {
                return NotFound();
            }
            //---------------------------------
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                byte[] bytes = Convert.FromBase64String(dto.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Foto = fileName;
            }
            if (!string.IsNullOrEmpty(dto.EmblemaBase64))
            {
                byte[] bytes = Convert.FromBase64String(dto.EmblemaBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Emblema = fileName;
            }
            //--------------------------------------
            mapper.Map(dto, Candidatos);
            Candidatos.Cargo = await context.Cargos.SingleOrDefaultAsync(c => c.Id == dto.Cargo.Id);
            context.Update(Candidatos);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidenciasExists(id))
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

        private bool IncidenciasExists(int id)
        {
            return context.Incidencias.Any(e => e.Id == id);
        }

    }
}