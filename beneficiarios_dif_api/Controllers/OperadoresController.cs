using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public OperadoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<OperadorDTO>> GetById(int id)
        {
            var operador = await context.Operadores
                .Include(s => s.Seccion)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (operador == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<OperadorDTO>(operador));
        }
        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<OperadorDTO>>> GetAll()
        {
            var operador = await context.Operadores
                .Include(s => s.Seccion)
                .ToListAsync();

            if (!operador.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<OperadorDTO>>(operador));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(OperadorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operador = mapper.Map<Operador>(dto);
            operador.Seccion = await context.Secciones.SingleOrDefaultAsync(i => i.Id == dto.Seccion.Id);

            context.Add(operador);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                return StatusCode(500, new { error = "Error al guardar el operador.", details = innerException?.Message });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { error = "Error al guardar el operador.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar el operador.", details = ex.Message });
            }
        }
        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var operador = await context.Operadores.FindAsync(id);

            if (operador == null)
            {
                return NotFound();
            }

            context.Operadores.Remove(operador);
            await context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, OperadorDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var Operadores = await context.Operadores.FindAsync(id);

            if (Operadores == null)
            {
                return NotFound();
            }

            mapper.Map(dto, Operadores);
            Operadores.Seccion = await context.Secciones.SingleOrDefaultAsync(i => i.Id == dto.Seccion.Id);
            context.Update(Operadores);

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
