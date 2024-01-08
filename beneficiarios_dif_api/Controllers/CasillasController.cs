using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/casillas")]
    [ApiController]
    public class CasillasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CasillasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<CasillaDTO>> GetById(int id)
        {
            var casilla = await context.Casillas
                .FirstOrDefaultAsync(u => u.Id == id);

            if (casilla == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CasillaDTO>(casilla));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<CasillaDTO>>> GetAll()
        {
            var casilla = await context.Casillas
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!casilla.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CasillaDTO>>(casilla));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(CasillaDTO dto)
        {
            // Validación del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificación de la existencia del usuario
            var existeCasilla = await context.Casillas.AnyAsync(u => u.Nombre == dto.Nombre);

            if (existeCasilla)
            {
                // return Conflict(new { error = "El usuario ya existe." });
                return Conflict();
            }

            // Mapeo del DTO a la entidad
            var casilla = mapper.Map<Casilla>(dto);
            
            // Incluir la entidad en el contexto
            context.Add(casilla);

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
            var casilla = await context.Casillas.FindAsync(id);

            if (casilla == null)
            {
                return NotFound();
            }

            context.Casillas.Remove(casilla);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CasillaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var casilla = await context.Casillas.FindAsync(id);

            if (casilla == null)
            {
                return NotFound();
            }

            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, casilla);

            context.Update(casilla);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasillaExists(id))
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

        private bool CasillaExists(int id)
        {
            return context.Casillas.Any(e => e.Id == id);
        }

    }
}