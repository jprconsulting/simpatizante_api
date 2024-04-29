using AutoMapper;
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
    [Route("api/voto")]
    [ApiController]
    [TokenValidationFilter]

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
            var voto = await context.votos
                 .Include(b => b.Simpatizante)
                .ThenInclude(b => b.Operador)
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
                var votos = await context.votos
                .Include(v => v.Simpatizante)
                    .ThenInclude(s => s.Municipio)
                .Include(v => v.Simpatizante)
                    .ThenInclude(s => s.Operador)
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

            var existeVoto = await context.votos.AnyAsync(n => n.Simpatizante.Id == dto.Simpatizante.Id);
            if (existeVoto)
                {
                return Conflict();                                 
                }

            var voto = mapper.Map<Voto>(dto);
            voto.FechaHoraVot = DateTime.Now;
            voto.Simpatizante = await context.simpatizantes.SingleOrDefaultAsync(s => s.Id == dto.Simpatizante.Id);


            context.votos.Add(voto);
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

            var voto = await context.votos.FindAsync(id);

            if (voto == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Foto = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioVotos);
            }
            else
            {
                dto.Foto = voto.Foto;
            }
            mapper.Map(dto, voto);
            voto.Simpatizante = await context.simpatizantes.SingleOrDefaultAsync(b => b.Id == dto.Simpatizante.Id);
            voto.FechaHoraVot = DateTime.Now;

            context.Update(voto);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.votos.Any(e => e.Id == id))
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
            var voto = await context.votos.FindAsync(id);

            if (voto == null)
            {
                return NotFound();
            }

            context.votos.Remove(voto);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}