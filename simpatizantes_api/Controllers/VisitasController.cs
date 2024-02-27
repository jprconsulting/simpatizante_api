using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Services;

namespace simpatizantes_api.Controllers
{
    [Authorize]
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
                    .Include(u => u.Usuario)
                    .ThenInclude(r => r.Rol)
                    .ToListAsync();

                if (!visitas.Any())
                {
                    return NotFound();
                }

                var visitasDTO = mapper.Map<List<VisitaDTO>>(visitas);

                foreach (var visitaDTO in visitasDTO)
                {
                    // Obtén la información del usuario desde la entidad Visita y asígnala al DTO
                    var usuario = visitas.FirstOrDefault(v => v.Id == visitaDTO.Id)?.Usuario;
                    if (usuario != null)
                    {
                        Console.WriteLine($"Nombre de usuario: {usuario.Nombre}");

                        visitaDTO.Usuario = mapper.Map<UsuarioDTO>(usuario);
                    }
                }

                return Ok(visitasDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(VisitaDTO dto)
        {
            var existevicita = await context.Visitas.AnyAsync(n => n.Simpatizante.Id == dto.Simpatizante.Id);
            if (existevicita)
            {
                return Conflict();
            }
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioVisitas);
            }

            int usuarioId = int.Parse(User.FindFirst("usuarioId")?.Value);

            var visita = mapper.Map<Visita>(dto);
            visita.FechaHoraVisita = DateTime.Now;
            visita.Usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
            visita.Simpatizante = await context.Simpatizantes.SingleOrDefaultAsync(s => s.Id == dto.Simpatizante.Id);


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

            visita.Servicio = dto.Servicio;
            visita.Descripcion = dto.Descripcion;
            visita.Simpatiza = dto.Simpatiza;

            // Verificar si se proporciona una nueva imagen
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                // Guardar la nueva imagen
                visita.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioVisitas);
            }

            visita.SimpatizanteId = dto.Simpatizante.Id;

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