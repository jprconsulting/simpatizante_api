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
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(c => c.Cargo)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (votante == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CandidatoDTO>(votante));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<CandidatoDTO>>> GetAll()
        {
            var votante = await context.Candidatos
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(c => c.Cargo)
                .ToListAsync();

            if (!votante.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CandidatoDTO>>(votante));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(CandidatoDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.ImagenBase64))
                {
                    byte[] imagenBytes = Convert.FromBase64String(dto.ImagenBase64);
                    string imagenFileName = $"{Guid.NewGuid()}.jpg";
                    string imagenFilePath = Path.Combine(webHostEnvironment.WebRootPath, "images", imagenFileName);
                    await System.IO.File.WriteAllBytesAsync(imagenFilePath, imagenBytes);
                    dto.Foto = imagenFileName;
                }

                if (!string.IsNullOrEmpty(dto.EmblemaBase64))
                {
                    byte[] emblemaBytes = Convert.FromBase64String(dto.EmblemaBase64);
                    string emblemaFileName = $"{Guid.NewGuid()}.jpg";
                    string emblemaFilePath = Path.Combine(webHostEnvironment.WebRootPath, "images", emblemaFileName);
                    await System.IO.File.WriteAllBytesAsync(emblemaFilePath, emblemaBytes);
                    dto.Emblema = emblemaFileName;
                }

                var cargo = await context.Cargos.FindAsync(dto.Cargo.Id);
                var municipio = await context.Municipios.FindAsync(dto.Municipio.Id);
                var seccion = await context.Secciones.FindAsync(dto.Seccion.Id);

                var candidato = new Candidato
                {
                    Nombres = dto.Nombres,
                    ApellidoPaterno = dto.ApellidoPaterno,
                    ApellidoMaterno = dto.ApellidoMaterno,
                    FechaNacimiento = dto.FechaNacimiento,
                    Sexo = dto.Sexo,
                    Sobrenombre = dto.Sobrenombre,
                    Foto = dto.Foto,
                    Emblema = dto.Emblema,
                    Estatus = dto.Estatus,
                    Cargo = cargo,
                    Municipio = municipio,
                    Seccion = seccion
                };

                context.Candidatos.Add(candidato);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while saving entity changes.";

                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner exception: {ex.InnerException.Message}";
                }

                return StatusCode(500, errorMessage);
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

            mapper.Map(dto, Candidatos);
            Candidatos.Seccion = await context.Secciones.SingleOrDefaultAsync(i => i.Id == dto.Seccion.Id);
            Candidatos.Municipio = await context.Municipios.SingleOrDefaultAsync(i => i.Id == dto.Municipio.Id);
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