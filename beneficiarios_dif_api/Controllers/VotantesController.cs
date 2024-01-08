using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/votantes")]
    [ApiController]
    public class VotantesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VotantesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<VotanteDTO>> GetById(int id)
        {
            var votante = await context.Votantes

                .Include(m => m.Municipio)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (votante == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VotanteDTO>(votante));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<VotanteDTO>>> GetAll()
        {
            var votantes = await context.Votantes
                .Include(m => m.Municipio)
                .Include(s => s.Seccion)
                .Include(e => e.Estado)
                .ToListAsync();

            if (!votantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<VotanteDTO>>(votantes));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(VotanteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeVotante = await context.Votantes.AnyAsync(b => b.Nombres == dto.Nombres && b.ApellidoPaterno == dto.ApellidoPaterno);

            if (existeVotante)
            {
                return Conflict();
            }

            var votante = mapper.Map<Votante>(dto);
            votante.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            votante.Seccion = await context.Secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            votante.Estado = await context.Estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);

            context.Add(votante);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar el votante.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var votante = await context.Votantes.FindAsync(id);

            if (votante == null)
            {
                return NotFound();
            }

            context.Votantes.Remove(votante);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, VotanteDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var votante = await context.Votantes.FindAsync(id);

            if (votante == null)
            {
                return NotFound();
            }

            mapper.Map(dto, votante);
            votante.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            context.Update(votante);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiarioExists(id))
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

        private bool BeneficiarioExists(int id)
        {
            return context.Votantes.Any(e => e.Id == id);
        }

    }
}