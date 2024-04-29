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
            var candidato = await context.candidatos
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
                var candidatos = await context.candidatos
                    .Include(t => t.Cargo)
                    .Include(t => t.Municipio)
                    .Include(t => t.Comunidad)
                    .Include(t => t.Estado)
                    .Include(t => t.Distrito)
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
            try
            {
                var existeCandidato = await context.candidatos.AnyAsync(n => n.Nombres == dto.Nombres &&
                                                                     n.ApellidoPaterno == dto.ApellidoPaterno &&
                                                                     n.ApellidoMaterno == dto.ApellidoMaterno);
                if (existeCandidato)
                {
                    return Conflict();
                }

                if (!string.IsNullOrEmpty(dto.ImagenBase64))
                {
                    dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioCandidatos);
                }

                if (!string.IsNullOrEmpty(dto.EmblemaBase64))
                {
                    dto.Emblema = await almacenadorImagenes.GuardarImagen(dto.EmblemaBase64, directorioEmblemas);
                }

                string nombreCompleto = User.FindFirst("nombreCompleto")?.Value;

                var distribucionCandidatura = mapper.Map<Candidato>(dto);
                distribucionCandidatura.Genero = await context.generos.SingleOrDefaultAsync(r => r.Id == dto.Genero.Id);
                distribucionCandidatura.Cargo = await context.cargos.SingleOrDefaultAsync(r => r.Id == dto.Cargo.Id);
                
                distribucionCandidatura.EstadoId = null;
                distribucionCandidatura.DistritoId = null;
                distribucionCandidatura.MunicipioId = null;
                distribucionCandidatura.ComunidadId = null;
                distribucionCandidatura.Distrito = null;
                distribucionCandidatura.Municipio = null;
                distribucionCandidatura.Comunidad = null;
                distribucionCandidatura.Estado = null;
                // Si es  Gubernatura
                if (dto.Cargo.Id == 7 || dto.Cargo.Id == 8 || dto.Cargo.Id == 9)
                {
                    distribucionCandidatura.Estado = await context.estados.SingleOrDefaultAsync(o => o.Id == dto.Estado.Id);
                }

                // Si es  Diputacion Local
                if (dto.Cargo.Id == 10)
                {
                    distribucionCandidatura.Distrito = await context.distritos.SingleOrDefaultAsync(c => c.Id == dto.Distrito.Id);
                }

                // Si es  Ayuntamientos
                if (dto.Cargo.Id == 11)
                {
                    distribucionCandidatura.Municipio = await context.municipios.SingleOrDefaultAsync(c => c.Id == dto.Municipio.Id);
                }

                // Si es Comunidad
                if (dto.Cargo.Id == 12)
                {
                    distribucionCandidatura.Comunidad = await context.comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);
                    distribucionCandidatura.Municipio = await context.municipios.SingleOrDefaultAsync(c => c.Id == dto.Municipio.Id);

                }

                context.Add(distribucionCandidatura);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Ocurrió un error al procesar la solicitud.", details = ex.InnerException?.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var candidato = await context.candidatos.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }

            var tieneDependencias = await context.operadores.AnyAsync(s => s.Id == id);

            if (tieneDependencias)
            {
                return StatusCode(502, new { error = "No se puede eliminar el simpatizante debido a dependencias existentes." });
            }

            context.candidatos.Remove(candidato);
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

            var candidato = await context.candidatos.FindAsync(id);
            
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
            candidato.Cargo = await context.cargos.SingleOrDefaultAsync(c => c.Id == dto.Cargo.Id);
            candidato.Genero = await context.generos.SingleOrDefaultAsync(g => g.Id == dto.Genero.Id);
            candidato.Distrito = null;
            candidato.DistritoId = null;
            candidato.Municipio = null;
            candidato.MunicipioId = null;
            candidato.Comunidad = null;
            candidato.ComunidadId = null;
            candidato.Estado = null;
            candidato.EstadoId = null;

            // Si es  Estado
            if (dto.Cargo.Id == 7 || dto.Cargo.Id == 8 || dto.Cargo.Id == 9)
            {                
                candidato.Estado = await context.estados.SingleOrDefaultAsync(o => o.Id == dto.Estado.Id);
                candidato.EstadoId = candidato.Estado.Id;
            }

            // Si es  Distrito
            if (dto.Cargo.Id == 10)
            {
                candidato.Distrito = await context.distritos.SingleOrDefaultAsync(c => c.Id == dto.Distrito.Id);
                candidato.DistritoId = candidato.Distrito.Id;
            }

            if (dto.Cargo.Id == 11)
            {
                candidato.Municipio = await context.municipios.SingleOrDefaultAsync(c => c.Id == dto.Municipio.Id);
                candidato.MunicipioId = candidato.Municipio.Id;
            }

            if (dto.Cargo.Id == 12)
            {
                candidato.Comunidad = await context.comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);
                candidato.ComunidadId = candidato.Comunidad.Id;
            }

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
            return context.incidencias.Any(e => e.Id == id);
        }

    }
}