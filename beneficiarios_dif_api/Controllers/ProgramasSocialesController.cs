using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/programas")]
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

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ProgramaSocialDTO>>> GetAll()
        {
            var ProgramasSociales = await context.ProgramasSociales.ToListAsync();

            if (!ProgramasSociales.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ProgramaSocialDTO>>(ProgramasSociales));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(ProgramaSocialDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existePrograma = await context.ProgramasSociales.AnyAsync(n => n.Nombre == dto.Nombre);

            if (existePrograma)
            {
                return Conflict();
            }

            var programa = mapper.Map<ProgramaSocial>(dto);

            context.Add(programa);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var programa = await context.ProgramasSociales.FindAsync(id);

            if (programa == null)
            {
                return NotFound();
            }

            context.ProgramasSociales.Remove(programa);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProgramaSocialDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var programa = await context.ProgramasSociales.FindAsync(id);

            if (programa == null)
            {
                return NotFound();
            }

            mapper.Map(dto, programa);

            context.Update(programa);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramaExists(id))
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

        private bool ProgramaExists(int id)
        {
            return context.ProgramasSociales.Any(e => e.Id == id);
        }
    }
}
