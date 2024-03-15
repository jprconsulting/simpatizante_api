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
    [Route("api/candidato")]
    [ApiController]
    [TokenValidationFilter]

    public class CandidatoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
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
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<CandidatoDTO>> GetById(int id)
        {
            var candidato = await context.Candidatos
                .Include(c => c.Cargo)
                .Include(g => g.Genero)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (candidato == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CandidatoDTO>(candidato));
        }     

        [HttpGet("obtener-todos")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var candidatos = await context.Candidatos
                    .Include(t => t.Cargo)
                    .Include(g => g.Genero)
                    .ToListAsync();

                if (!candidatos.Any())
                {
                    return NotFound();
                }
               
                return Ok(mapper.Map<List<CandidatoDTO>>(candidatos));
            }           
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del candidatos ", details = ex.Message });
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(CandidatoDTO dto)
        {
            var existeCandidato = await context.Candidatos.AnyAsync(n => n.Nombres == dto.Nombres &&
                                                                 n.ApellidoPaterno == dto.ApellidoPaterno &&
                                                                 n.ApellidoMaterno == dto.ApellidoMaterno);
            if (existeCandidato)
            {
                return Conflict();
            }
            try
            {
                if (!string.IsNullOrEmpty(dto.ImagenBase64))
                {                   
                    dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioCandidatos);
                }

                if (!string.IsNullOrEmpty(dto.EmblemaBase64))                {
                 
                    dto.Emblema = await almacenadorImagenes.GuardarImagen(dto.EmblemaBase64, directorioEmblemas);
                }

                string nombreCompleto = User.FindFirst("nombreCompleto")?.Value;

                var candidato = mapper.Map<Candidato>(dto);
                candidato.UsuarioCreacionNombre = nombreCompleto; // Establecer el UsuarioCreacionId
                candidato.FechaHoraCreacion = DateTime.Now; // Establecer la fecha de creación
                candidato.Cargo = await context.Cargos.SingleOrDefaultAsync(b => b.Id == dto.Cargo.Id);
                candidato.Genero = await context.Generos.SingleOrDefaultAsync(g => g.Id == dto.Genero.Id);

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

            var tieneDependencias = await context.Operadores.AnyAsync(s => s.Id == id);

            if (tieneDependencias)
            {
                return StatusCode(502, new { error = "No se puede eliminar el simpatizante debido a dependencias existentes." });
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

            var candidato = await context.Candidatos.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioCandidatos);
            }
            else
            {
                dto.Foto = candidato.Foto;
            }


            if (!string.IsNullOrEmpty(dto.EmblemaBase64))
            {

                dto.Emblema = await almacenadorImagenes.GuardarImagen(dto.EmblemaBase64, directorioEmblemas);
            }
            else
            {
                dto.Emblema = candidato.Emblema;
            }
            
            mapper.Map(dto, candidato);
            candidato.Cargo = await context.Cargos.SingleOrDefaultAsync(c => c.Id == dto.Cargo.Id);
            candidato.Genero = await context.Generos.SingleOrDefaultAsync(g => g.Id == dto.Genero.Id);

            context.Update(candidato);

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