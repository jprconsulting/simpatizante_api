using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Services;

namespace simpatizantes_api.Controllers
{
    [Route("api/candidato")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioCandidatos = "candidatos";
        private readonly string directorioEmblemas = "emblemas";


        public CandidatoController(
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
        public async Task<ActionResult<CandidatoDTO>> GetById(int id)
        {
            var votante = await context.Candidatos
                .Include(c => c.Cargo)
                .Include(s => s.Simpatizantes)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (votante == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CandidatoDTO>(votante));
        }     

        [HttpGet("obtener-todos")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var candidatos = await context.Candidatos
                    .Include(t => t.Cargo)
                    .Include(s => s.Simpatizantes)
                    .ToListAsync();

                if (!candidatos.Any())
                {
                    return NotFound();
                }
               
                return Ok(mapper.Map<List<CandidatoDTO>>(candidatos));
            }           
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(CandidatoDTO dto)
        {

            try
            {
                if (!string.IsNullOrEmpty(dto.ImagenBase64))
                {                   
                    dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioCandidatos);
                }

                if (!string.IsNullOrEmpty(dto.EmblemaBase64))                {
                 
                    dto.Emblema = await almacenadorImagenes.GuardarImagen(dto.EmblemaBase64, directorioEmblemas);
                }
                var candidato = mapper.Map<Candidato>(dto);
                candidato.Cargo = await context.Cargos.SingleOrDefaultAsync(b => b.Id == dto.Cargo.Id);

                context.Candidatos.Add(candidato);
                await context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
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