using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Services;

namespace simpatizantes_api.Controllers
{
    [Route("api/voto")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioVotos = "votos";

        public VotoController(
            ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<VotoDTO>> GetById(int id)
        {
            var voto = await context.Votos
                 .Include(b => b.Simpatizante)
                 .FirstOrDefaultAsync(v => v.Id == id);

            if (voto == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VotoDTO>(voto));
        }

    [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<VotoDTO>>> GetAll()
        {
            try
            {
                var votos = await context.Votos
                .Include(v => v.Simpatizante)
                .ThenInclude(b => b.Municipio)
                .ToListAsync();

                if (!votos.Any())
                {
                    return NotFound();
                }

                return Ok(mapper.Map<List<VotoDTO>>(votos));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(VotoDTO dto)
        {

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioVotos);
            }

            var voto = mapper.Map<Voto>(dto);
            voto.FechaHoraVot = DateTime.Now;
            voto.Simpatizante = await context.Simpatizantes.SingleOrDefaultAsync(s => s.Id == dto.Simpatizante.Id);


            context.Votos.Add(voto);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, VotoDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var visita = await context.Votos.FindAsync(id);

            if (visita == null)
            {
                return NotFound();
            }

            visita.Simpatizante = await context.Simpatizantes.SingleOrDefaultAsync(c => c.Id == dto.Simpatizante.Id);


            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioVotos);
            }

            mapper.Map(dto, visita);
            visita.Simpatizante = await context.Simpatizantes.SingleOrDefaultAsync(b => b.Id == dto.Simpatizante.Id);

            context.Update(visita);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Votos.Any(e => e.Id == id))
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

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var voto = await context.Votos.FindAsync(id);

            if (voto == null)
            {
                return NotFound();
            }

            context.Votos.Remove(voto);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}