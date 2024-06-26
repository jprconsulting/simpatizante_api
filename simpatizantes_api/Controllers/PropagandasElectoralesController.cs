﻿using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Services;
using simpatizantes_api.Filters;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/propagandas")]
    [ApiController]
    [TokenValidationFilter]

    public class PropagandasElectoralesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioPropagandas = "propagandas"; 

        public PropagandasElectoralesController(
            ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<PropagandaElectoralDTO>> GetById(int id)
        {
            var propaganda = await context.propagandaselectorales
                .Include(b => b.Municipio)
                .Include(s => s.Seccion)
                .Include(c => c.Candidato)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (propaganda == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PropagandaElectoralDTO>(propaganda));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<PropagandaElectoralDTO>>> GetAll()
        {
            try
            {
                var propaganda = await context.propagandaselectorales
                    .Include(v => v.Municipio)
                    .Include(s => s.Seccion)
                    .Include(c => c.Candidato)
                    .ToListAsync();

                if (!propaganda.Any())
                {
                    return NotFound();
                }

                var propagandasDTO = mapper.Map<List<PropagandaElectoralDTO>>(propaganda);

                return Ok(propagandasDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(PropagandaElectoralDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioPropagandas);
            }


            var propaganda = mapper.Map<PropagandaElectoral>(dto);
            propaganda.Municipio = await context.municipios.SingleOrDefaultAsync(s => s.Id == dto.Municipio.Id);
            propaganda.Seccion = await context.secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            propaganda.Candidato = await context.candidatos.SingleOrDefaultAsync(s => s.Id == dto.Candidato.Id);

            context.propagandaselectorales.Add(propaganda);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var propaganda = await context.propagandaselectorales.FindAsync(id);

            if (propaganda == null)
            {
                return NotFound();
            }

            context.propagandaselectorales.Remove(propaganda);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, PropagandaElectoralDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var propaganda = await context.propagandaselectorales.FindAsync(id);

            if (propaganda == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioPropagandas);
            }
            else
            {
                dto.Foto = propaganda.Foto;
            }

            mapper.Map(dto, propaganda);
            propaganda.Municipio = await context.municipios.SingleOrDefaultAsync(c => c.Id == dto.Municipio.Id);
            propaganda.Seccion = await context.secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            propaganda.Candidato = await context.candidatos.SingleOrDefaultAsync(s => s.Id == dto.Candidato.Id);

            context.Update(propaganda);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropagandasExists(id))
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

        private bool PropagandasExists(int id)
        {
            return context.propagandaselectorales.Any(e => e.Id == id);
        }

    }
}