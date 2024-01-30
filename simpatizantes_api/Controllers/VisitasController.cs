using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Services;

namespace simpatizantes_api.Controllers
{
    [Route("api/visitas")]
    [ApiController]
    public class VisitasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioVisitas = "visitas";

        public VisitasController(
            ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
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

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<VisitaDTO>>> GetAll()
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

                return Ok(mapper.Map<List<VisitaDTO>>(visitas));
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
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioVisitas);
            }

            int currentUserRolId = int.Parse(User.FindFirst("rolId")?.Value);

            var visita = mapper.Map<Visita>(dto);
            visita.FechaHoraVisita = DateTime.Now;
            visita.Simpatizante = await context.Simpatizantes.SingleOrDefaultAsync(s => s.Id == dto.Simpatizante.Id);
            visita.Operador = null;
            visita.OperadorId = null;
            visita.Candidato = null;
            visita.CandidatoId = null;

            // Si es operador
            if (currentUserRolId == 2)
            {
                if (int.TryParse(User.FindFirst("operadorId")?.Value, out int parsedOperadorId))
                {
                    visita.Operador = await context.Operadores.FirstOrDefaultAsync(o => o.Id == parsedOperadorId);
                }
            }

            // Si es candidato
            if (currentUserRolId == 3)
            {
                if (int.TryParse(User.FindFirst("candidatoId")?.Value, out int parsedCandidatoId))
                {
                    visita.Candidato = await context.Candidatos.FirstOrDefaultAsync(c => c.Id == parsedCandidatoId);
                }
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

            if (visita == null)
            {
                return NotFound();
            }

            int currentUserRolId = int.Parse(User.FindFirst("rolId")?.Value);
            visita.Simpatizante = await context.Simpatizantes.SingleOrDefaultAsync(c => c.Id == dto.Simpatizante.Id);
            visita.Operador = null;
            visita.OperadorId = null;
            visita.Candidato = null;
            visita.CandidatoId = null;

            // Si es operador
            if (currentUserRolId == 2)
            {
                if (int.TryParse(User.FindFirst("operadorId")?.Value, out int parsedOperadorId))
                {
                    visita.Operador = await context.Operadores.FirstOrDefaultAsync(o => o.Id == parsedOperadorId);
                }
            }

            // Si es candidato
            if (currentUserRolId == 3)
            {
                if (int.TryParse(User.FindFirst("candidatoId")?.Value, out int parsedCandidatoId))
                {
                    visita.Candidato = await context.Candidatos.FirstOrDefaultAsync(c => c.Id == parsedCandidatoId);
                }
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioVisitas);
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