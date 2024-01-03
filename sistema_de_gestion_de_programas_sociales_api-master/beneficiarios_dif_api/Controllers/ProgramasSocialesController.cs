using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Authorize]
    [Route("api/programas-sociales")]
    [ApiController]
    public class ProgramasSocialesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProgramasSocialesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<ProgramaSocialDTO>> GetById(int id)
        {
            var programaSocial = await context.ProgramasSociales
                .Include(a => a.AreaAdscripcion)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (programaSocial == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProgramaSocialDTO>(programaSocial));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ProgramaSocialDTO>>> GetAll()
        {
            var programasSociales = await context.ProgramasSociales
                .Include(a => a.AreaAdscripcion)
                .ToListAsync();

            if (!programasSociales.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ProgramaSocialDTO>>(programasSociales));
        }       

        [HttpPost("crear")]
        public async Task<ActionResult> Post(ProgramaSocialDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeProgramaSocial = await context.ProgramasSociales.AnyAsync(p => p.Nombre == dto.Nombre);

            if (existeProgramaSocial)
            {
                return Conflict();
            }

            var programaSocial = mapper.Map<ProgramaSocial>(dto);
            programaSocial.AreaAdscripcion = await context.AreasAdscripcion.SingleOrDefaultAsync(a => a.Id == dto.AreaAdscripcion.Id);
            context.Add(programaSocial);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor.", details = ex.Message });
            }
        }     


        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var programaSocial = await context.ProgramasSociales.FindAsync(id);

            if (programaSocial == null)
            {
                return NotFound();
            }

            context.ProgramasSociales.Remove(programaSocial);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, ProgramaSocialDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var programaSocial = await context.ProgramasSociales.FindAsync(id);

            if (programaSocial == null)
            {
                return NotFound();
            }

            mapper.Map(dto, programaSocial);
            programaSocial.AreaAdscripcion = await context.AreasAdscripcion.SingleOrDefaultAsync(a => a.Id == dto.AreaAdscripcion.Id);
            context.Update(programaSocial);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramaSocialExists(id))
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

        private bool ProgramaSocialExists(int id)
        {
            return context.ProgramasSociales.Any(e => e.Id == id);
        }
    }
}
