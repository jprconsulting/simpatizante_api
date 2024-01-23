using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UsuarioDTO>(usuario));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<UsuarioDTO>>> GetAll()
        {
            var usuarios = await context.Usuarios
                .Include(i => i.Rol)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!usuarios.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<UsuarioDTO>>(usuarios));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = mapper.Map<Usuario>(dto);
            usuario.Rol = await context.Rols.SingleOrDefaultAsync(b => b.Id == dto.Rol.Id);
            context.Add(usuario);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                return StatusCode(500, new { error = "Error al guardar el usuario.", details = innerException?.Message });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { error = "Error al guardar el usuario.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar al usuario.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, usuario);
            usuario.Rol = await context.Rols.SingleOrDefaultAsync(r => r.Id == dto.Rol.Id);

            context.Update(usuario);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        private bool UsuarioExists(int id)
        {
            return context.Usuarios.Any(e => e.Id == id);
        }

    }
}