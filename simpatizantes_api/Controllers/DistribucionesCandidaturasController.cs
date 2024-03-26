using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using simpatizantes_api.Filters;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/distribucion-candidaturas")]
    [ApiController]
    [TokenValidationFilter]

    public class DistribucionesCandidaturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
         
        public DistribucionesCandidaturasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<DistribucionCandidaturaDTO>> GetById(int id)
        {
            var distribucionCandidatura = await context.DistribucionesCandidaturas
                .Include(u => u.TipoEleccion)
                .Include(u => u.Distrito)
                .Include(u => u.Municipio)
                .Include(u => u.Comunidad)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (distribucionCandidatura == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DistribucionCandidaturaDTO>(distribucionCandidatura));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<DistribucionCandidaturaDTO>>> GetAll()
        {
            var distribucionCandidatura = await context.DistribucionesCandidaturas
                .Include(u => u.TipoEleccion)
                .Include(u => u.Distrito)
                .Include(u => u.Municipio)
                .Include(u => u.Comunidad)
                .ToListAsync();

            if (!distribucionCandidatura.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<DistribucionCandidaturaDTO>>(distribucionCandidatura));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(DistribucionCandidaturaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var distribucionCandidatura = mapper.Map<DistribucionCandidatura>(dto);
                distribucionCandidatura.TipoEleccion = await context.TiposElecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);
                distribucionCandidatura.DistritoId = null;
                distribucionCandidatura.MunicipioId = null;
                distribucionCandidatura.ComunidadId = null;
                distribucionCandidatura.Distrito = null;
                distribucionCandidatura.Municipio = null;
                distribucionCandidatura.Comunidad = null;

                // Si es  Diputacion Local
                if (dto.TipoEleccion.Id == 4)
                {
                    distribucionCandidatura.Distrito = await context.Distritos.SingleOrDefaultAsync(c => c.Id == dto.Distrito.Id);
                }

                // Si es  Ayuntamientos
                if (dto.TipoEleccion.Id == 5)
                {
                    distribucionCandidatura.Municipio = await context.Municipios.SingleOrDefaultAsync(c => c.Id == dto.Municipio.Id);
                }

                // Si es Comunidad
                if (dto.TipoEleccion.Id == 6)
                {
                    distribucionCandidatura.Comunidad = await context.Comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);

                    distribucionCandidatura.Partidos = string.Join(",", dto.Partidos.Select(p => p.ToString()));
                }

                else
                {
                    if (dto.Partidos == null || dto.Partidos.Count == 0)
                    {
                        return BadRequest("Debe proporcionar al menos un partido para el tipo de agrupación política seleccionado.");
                    }

                    // Convierte los objetos CandidaturaDTO a entidades Candidatura y añádelos a la lista de Partidos en la entidad Candidatura
                    distribucionCandidatura.Partidos = string.Join(",", dto.Partidos);
                    distribucionCandidatura.Coalicion = string.Join(",", dto.Coalicion);
                    distribucionCandidatura.Comun = string.Join(",", dto.Comun);
                    distribucionCandidatura.Independiente = string.Join(",", dto.Independiente);
                }
                context.Add(distribucionCandidatura);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar la distribución.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var distribucionCandidatura = await context.DistribucionesCandidaturas.FindAsync(id);

            if (distribucionCandidatura == null)
            {
                return NotFound();
            }

            context.DistribucionesCandidaturas.Remove(distribucionCandidatura);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] DistribucionCandidaturaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var distribucionCandidatura = await context.DistribucionesCandidaturas.FindAsync(id);

            var currentDistritoId = distribucionCandidatura?.DistritoId;
            var currentMunicipioId = distribucionCandidatura?.MunicipioId;
            var currentComunidadId = distribucionCandidatura?.ComunidadId;

            if (distribucionCandidatura == null)
            {
                return NotFound();
            }

            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, distribucionCandidatura);
            distribucionCandidatura.TipoEleccion = await context.TiposElecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);

            distribucionCandidatura.Distrito = null;
            distribucionCandidatura.DistritoId = null;
            distribucionCandidatura.Municipio = null;
            distribucionCandidatura.MunicipioId = null;
            distribucionCandidatura.Comunidad = null;
            distribucionCandidatura.ComunidadId = null;

            // Si es  Gubernatura
            if (dto.TipoEleccion.Id == 1)
            {
                if (currentDistritoId != null && currentDistritoId != dto.Distrito.Id)
                {
                    var existsUserOperador = await context.DistribucionesCandidaturas.AnyAsync(c => c.Distrito.Id == dto.Distrito.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }

                distribucionCandidatura.Distrito = await context.Distritos.SingleOrDefaultAsync(o => o.Id == dto.Distrito.Id);
                distribucionCandidatura.DistritoId = distribucionCandidatura.Distrito.Id;
            }

            // Si es  Diputados Locales
            if (dto.TipoEleccion.Id == 2)
            {
                if (currentDistritoId != null && currentDistritoId != dto.Distrito.Id)
                {
                    var existsUserOperador = await context.DistribucionesCandidaturas.AnyAsync(c => c.Distrito.Id == dto.Distrito.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
            }
            distribucionCandidatura.Distrito = await context.Distritos.SingleOrDefaultAsync(o => o.Id == dto.Distrito.Id);
            distribucionCandidatura.DistritoId = distribucionCandidatura.Distrito.Id;

            // Si es  Ayuntamientos
            if (dto.TipoEleccion.Id == 3)
            {
                if (currentMunicipioId != null && currentMunicipioId != dto.Municipio.Id)
                {
                    var existsUserOperador = await context.DistribucionesCandidaturas.AnyAsync(c => c.Municipio.Id == dto.Municipio.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
            }
            distribucionCandidatura.Municipio = await context.Municipios.SingleOrDefaultAsync(o => o.Id == dto.Municipio.Id);
            distribucionCandidatura.MunicipioId = distribucionCandidatura.Municipio.Id;

            // Si es  Comunidad
            if (dto.TipoEleccion.Id == 4)
            {
                if (currentComunidadId != null && currentComunidadId != dto.Comunidad.Id)
                {
                    var existsUserOperador = await context.DistribucionesCandidaturas.AnyAsync(c => c.Comunidad.Id == dto.Comunidad.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
            }
            distribucionCandidatura.Comunidad = await context.Comunidades.SingleOrDefaultAsync(o => o.Id == dto.Comunidad.Id);
            distribucionCandidatura.ComunidadId = distribucionCandidatura.Comunidad.Id;

            context.Update(distribucionCandidatura);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistribucionCandidaturaExists(id))
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

        private bool DistribucionCandidaturaExists(int id)
        {
            return context.DistribucionesCandidaturas.Any(e => e.Id == id);
        }

    }
}