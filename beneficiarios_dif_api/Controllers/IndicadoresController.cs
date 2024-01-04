using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/indicadores")]
    [ApiController]
    public class IndicadoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public IndicadoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<IndicadorDTO>> GetById(int id)
        {
            var indicador = await context.Indicadores.FindAsync(id);

            if (indicador == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IndicadorDTO>(indicador));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<IndicadorDTO>>> GetAll()
        {
            var indicadores = await context.Indicadores.ToListAsync();

            if (!indicadores.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<IndicadorDTO>>(indicadores));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(IndicadorDTO dto)
        {
            var indicador = mapper.Map<Indicador>(dto);

            context.Indicadores.Add(indicador);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var indicador = await context.Indicadores.FindAsync(id);

            if (indicador == null)
            {
                return NotFound();
            }

            context.Indicadores.Remove(indicador);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] IndicadorDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var indicador = await context.Indicadores.FindAsync(id);

            if (indicador == null)
            {
                return NotFound();
            }

            mapper.Map(dto, indicador);
            context.Update(indicador);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicadorExists(id))
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

        private bool IndicadorExists(int id)
        {
            return context.Indicadores.Any(e => e.Id == id);
        }
    }
}
