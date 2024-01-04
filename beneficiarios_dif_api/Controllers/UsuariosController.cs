using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Authorize]
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
                .Include(u => u.AreaAdscripcion)
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
                .Include(u => u.AreaAdscripcion)
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
            // Validación del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificación de la existencia del usuario
            var existeUsuario = await context.Usuarios.AnyAsync(u => u.Nombre == dto.Nombre && u.ApellidoPaterno == dto.ApellidoPaterno);

            if (existeUsuario)
            {
                // return Conflict(new { error = "El usuario ya existe." });
                return Conflict();
            }

            // Mapeo del DTO a la entidad
            var usuario = mapper.Map<Usuario>(dto);

            // Asociar el rol
            usuario.Rol = await context.Rols.SingleOrDefaultAsync(r => r.Id == dto.Rol.Id);
            usuario.AreaAdscripcion = null;

            // Solo si tiene rol de 'director' se agrega área de adscripción
            if (dto.Rol.Id == 1)
            {
                usuario.AreaAdscripcion = await context.AreasAdscripcion.SingleOrDefaultAsync(a => a.Id == dto.AreaAdscripcion.Id);
            }

            // Incluir la entidad en el contexto
            context.Add(usuario);

            try
            {
                // Guardar cambios en la base de datos dentro de una transacción
                await context.SaveChangesAsync();
                return Ok();
                // return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejar errores de base de datos
                // return StatusCode(500, new { error = "Error interno del servidor.", details = ex.Message });
                return StatusCode(500);
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
            usuario.AreaAdscripcion = null;

            // Solo si tiene rol de 'director' se agrega área de adscripción
            if (dto.Rol.Id == 1)
            {
                usuario.AreaAdscripcion = await context.AreasAdscripcion.SingleOrDefaultAsync(a => a.Id == dto.AreaAdscripcion.Id);
            }

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
