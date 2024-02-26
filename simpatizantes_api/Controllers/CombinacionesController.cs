﻿using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace simpatizantes_api.Controllers
{
    [Route("api/combinaciones")]
    [ApiController]
    public class CombinacionesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CombinacionesController (ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<CombinacionDTO>> GetById(int id)
        {
            var combinacion = await context.Combinaciones
                .Include(u => u.Candidatura)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (combinacion == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CombinacionDTO>(combinacion));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<CombinacionDTO>>> GetAll()
        {
            var combinacion = await context.Combinaciones
                .Include(u => u.Candidatura)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!combinacion.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<CombinacionDTO>>(combinacion));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(CombinacionDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var combinacion = mapper.Map<Combinacion>(dto);
                combinacion.Candidatura = await context.Candidaturas.SingleOrDefaultAsync(r => r.Id == dto.Candidatura.Id);

                context.Add(combinacion);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar la combinacion.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var combinacion = await context.Combinaciones.FindAsync(id);

            if (combinacion == null)
            {
                return NotFound();
            }

            context.Combinaciones.Remove(combinacion);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CombinacionDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var combinacion = await context.Combinaciones.FindAsync(id);

            if (combinacion == null)
            {
                return NotFound();
            }

            mapper.Map(dto, combinacion);
            combinacion.Candidatura = await context.Candidaturas.SingleOrDefaultAsync(r => r.Id == dto.Candidatura.Id);

            context.Update(combinacion);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CombinacionesExists(id))
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

        private bool CombinacionesExists(int id)
        {
            return context.Combinaciones.Any(e => e.Id == id);
        }

    }
}