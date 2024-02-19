using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/promotores")]
    [ApiController]
    public class PromotoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PromotoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("por-operador/{operadorId:int}")]
        public async Task<ActionResult<List<PromotorDTO>>> GetByOperadorId(int operadorId)
        {
            var promotores = await context.Promotores                
                .Where(p => p.PromotorOperadores.Any(po => po.Operador.Id == operadorId))
                .ToListAsync();

            if (!promotores.Any())
            {
                return NotFound();
            }

            var promotoresDTO = mapper.Map<List<PromotorDTO>>(promotores);

            foreach (var promotor in promotoresDTO)
            {
                var operadores = await context.PromotoresOperadores
                    .Include(p => p.Operador)
                    .Where(po => po.Promotor.Id == promotor.Id)
                    .Select(i => i.Operador)
                    .ToListAsync();

                promotor.Operadores = mapper.Map<List<OperadorDTO>>(operadores);
            }

            return Ok(promotoresDTO);
        }

        [HttpGet("por-candidato/{candidatoId:int}")]
        public async Task<ActionResult<List<PromotorDTO>>> GetByCandidatoId(int candidatoId)
        {
            var promotores = await context.Promotores
                .Where(p => p.PromotorOperadores.Any(po => po.Operador.CandidatoId == candidatoId))
                .ToListAsync();

            if (!promotores.Any())
            {
                return NotFound();
            }

            var promotoresDTO = mapper.Map<List<PromotorDTO>>(promotores);

            foreach (var promotor in promotoresDTO)
            {
                var operadores = await context.PromotoresOperadores
                    .Include(p => p.Operador)
                    .Where(po => po.Promotor.Id == promotor.Id)
                    .Select(i => i.Operador)
                    .ToListAsync();

                promotor.Operadores = mapper.Map<List<OperadorDTO>>(operadores);
            }

            return Ok(promotoresDTO);
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<PromotorDTO>>> GetAll()
        {
            var promotores = await context.Promotores.ToListAsync();

            if (!promotores.Any())
            {
                return NotFound();
            }

            var promotoresDTO = mapper.Map<List<PromotorDTO>>(promotores);

            foreach (var promotor in promotoresDTO)
            {
                var operadores = await context.PromotoresOperadores
                    .Include(p => p.Operador)
                    .Where(po => po.Promotor.Id == promotor.Id)
                    .Select(i => i.Operador)
                    .ToListAsync();

                promotor.Operadores = mapper.Map<List<OperadorDTO>>(operadores);
            }

            return Ok(promotoresDTO);
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(PromotorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existePromotor = await context.Promotores.AnyAsync(n => n.Nombres == dto.Nombres &&
                                                                   n.ApellidoPaterno == dto.ApellidoPaterno &&
                                                                   n.ApellidoMaterno == dto.ApellidoMaterno);
            if (existePromotor)
            {
                return Conflict();
            }

            int usuarioId = int.Parse(User.FindFirst("usuarioId")?.Value); // Obtener el Id del usuario actual

            // Mapear el DTO a la entidad Promotor
            var promotor = mapper.Map<Promotor>(dto);

            // Establecer el UsuarioCreacionId y la FechaHoraCreacion
            promotor.UsuarioCreacionId = usuarioId;
            promotor.FechaHoraCreacion = DateTime.Now;

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    context.Add(promotor);

                    if (await context.SaveChangesAsync() > 0)
                    {
                        foreach (var operadorId in dto.OperadoresIds)
                        {
                            var existsOperador = await context.Operadores.SingleOrDefaultAsync(o => o.Id == operadorId);
                            var existsPromotorOperador = await context.PromotoresOperadores.AnyAsync(po => po.Promotor.Id == promotor.Id && po.Operador.Id == operadorId);

                            if (existsOperador != null && !existsPromotorOperador)
                            {
                                var promotorOperador = new PromotorOperador
                                {
                                    Promotor = promotor,
                                    Operador = existsOperador
                                };
                                context.Add(promotorOperador);
                            }
                        }

                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return Ok();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { error = "Error interno del servidor al guardar el promotor.", details = ex.Message });
                }
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var promotor = await context.Promotores.FindAsync(id);

            if (promotor == null)
            {
                return NotFound();
            }

            var tieneDependencias = await context.Simpatizantes.AnyAsync(s => s.Promotor.Id == id);

            if (tieneDependencias)
            {
                return StatusCode(502, new { error = "No se puede eliminar el promotor debido a dependencias existentes." });
            }

            context.Promotores.Remove(promotor);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PromotorDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }


            var promotor = await context.Promotores
               .Include(p => p.PromotorOperadores)
               .FirstOrDefaultAsync(p => p.Id == id);

            if (promotor == null)
            {
                return NotFound();
            }

            // Actualizar propiedades de nombres y apellidos
            promotor.Nombres = dto.Nombres;
            promotor.ApellidoPaterno = dto.ApellidoPaterno;
            promotor.ApellidoMaterno = dto.ApellidoMaterno;
            promotor.Telefono = dto.Telefono;

            // Limpiar todos las operadores actuales
            promotor.PromotorOperadores.Clear();

            // Agregar las nuevas secciones
            foreach (var operadorId in dto.OperadoresIds)
            {
                var operador = await context.Operadores.FindAsync(operadorId);
                if (operador != null)
                {
                    promotor.PromotorOperadores.Add(new PromotorOperador { PromotorId = id, OperadorId = operadorId, Promotor = promotor, Operador = operador });
                }
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotorExists(id))
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

        private bool PromotorExists(int id)
        {
            return context.Promotores.Any(e => e.Id == id);
        }
    }
}
