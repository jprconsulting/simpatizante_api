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
    [Route("api/resultados-pre-eliminares")]
    [ApiController]
    [TokenValidationFilter]

    public class ResultadosPreEliminaresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ResultadosPreEliminaresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<ResultadoPreEliminarDTO>> GetById(int id)
        {
            var resultadoPreEliminar = await context.ResultadosPreEliminares
                .Include(u => u.TipoEleccion)
                .Include(u => u.Distrito)
                .Include(u => u.Municipio)
                .Include(u => u.Comunidad)
                .Include(u => u.Seccion)
                .Include(u => u.Casilla)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (resultadoPreEliminar == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ResultadoPreEliminarDTO>(resultadoPreEliminar));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ResultadoPreEliminarDTO>>> GetAll()
        {
            var resultadoPreEliminar = await context.ResultadosPreEliminares
                .Include(u => u.TipoEleccion)
                .Include(u => u.Distrito)
                .Include(u => u.Municipio)
                .Include(u => u.Comunidad)
                .Include(u => u.Seccion)
                .Include(u => u.Casilla)
                .ToListAsync();

            if (!resultadoPreEliminar.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ResultadoPreEliminarDTO>>(resultadoPreEliminar));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(ResultadoPreEliminarDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var resultadoPreEliminar = mapper.Map<ResultadoPreEliminar>(dto);
                resultadoPreEliminar.TipoEleccion = await context.TiposElecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);
                resultadoPreEliminar.Seccion = await context.Secciones.SingleOrDefaultAsync(r => r.Id == dto.Seccion.Id);
                resultadoPreEliminar.Casilla = await context.Casillas.SingleOrDefaultAsync(r => r.Id == dto.Casilla.Id);
                resultadoPreEliminar.DistritoId = null;
                resultadoPreEliminar.MunicipioId = null;
                resultadoPreEliminar.ComunidadId = null;
                resultadoPreEliminar.Distrito = null;
                resultadoPreEliminar.Municipio = null;
                resultadoPreEliminar.Comunidad = null;
                // Si es  Gubernatura
                if (dto.TipoEleccion.Id == 1)
                {
                    resultadoPreEliminar.Distrito = await context.Distritos.SingleOrDefaultAsync(o => o.Id == dto.Distrito.Id);
                }

                // Si es  Diputacion Local
                if (dto.TipoEleccion.Id == 2)
                {
                    resultadoPreEliminar.Distrito = await context.Distritos.SingleOrDefaultAsync(c => c.Id == dto.Distrito.Id);
                }

                // Si es  Ayuntamientos
                if (dto.TipoEleccion.Id == 3)
                {
                    resultadoPreEliminar.Municipio = await context.Municipios.SingleOrDefaultAsync(c => c.Id == dto.Municipio.Id);
                }

                // Si es Comunidad
                if (dto.TipoEleccion.Id == 4)
                {
                    resultadoPreEliminar.Comunidad = await context.Comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);

                    resultadoPreEliminar.Partidos = string.Join(",", dto.Partidos.Select(p => p.ToString()));
                }

                else
                {
                    if (dto.Partidos == null || dto.Partidos.Count == 0)
                    {
                        return BadRequest("Debe proporcionar al menos un partido para el tipo de agrupación política seleccionado.");
                    }

                    // Convierte los objetos CandidaturaDTO a entidades Candidatura y añádelos a la lista de Partidos en la entidad Candidatura
                    resultadoPreEliminar.Partidos = string.Join(",", dto.Partidos);
                }
                context.Add(resultadoPreEliminar);
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
            var resultadoPreEliminar = await context.ResultadosPreEliminares.FindAsync(id);

            if (resultadoPreEliminar == null)
            {
                return NotFound();
            }

            context.ResultadosPreEliminares.Remove(resultadoPreEliminar);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ResultadoPreEliminarDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var resultadoPreEliminar = await context.ResultadosPreEliminares.FindAsync(id);

            var currentDistritoId = resultadoPreEliminar?.DistritoId;
            var currentMunicipioId = resultadoPreEliminar?.MunicipioId;
            var currentComunidadId = resultadoPreEliminar?.ComunidadId;

            if (resultadoPreEliminar == null)
            {
                return NotFound();
            }

            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, resultadoPreEliminar);
            resultadoPreEliminar.TipoEleccion = await context.TiposElecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);
            resultadoPreEliminar.Seccion = await context.Secciones.SingleOrDefaultAsync(r => r.Id == dto.Seccion.Id);
            resultadoPreEliminar.Casilla = await context.Casillas.SingleOrDefaultAsync(r => r.Id == dto.Casilla.Id);

            resultadoPreEliminar.Distrito = null;
            resultadoPreEliminar.DistritoId = null;
            resultadoPreEliminar.Municipio = null;
            resultadoPreEliminar.MunicipioId = null;
            resultadoPreEliminar.Comunidad = null;
            resultadoPreEliminar.ComunidadId = null;

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

                resultadoPreEliminar.Distrito = await context.Distritos.SingleOrDefaultAsync(o => o.Id == dto.Distrito.Id);
                resultadoPreEliminar.DistritoId = resultadoPreEliminar.Distrito.Id;
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
            resultadoPreEliminar.Distrito = await context.Distritos.SingleOrDefaultAsync(o => o.Id == dto.Distrito.Id);
            resultadoPreEliminar.DistritoId = resultadoPreEliminar.Distrito.Id;

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
            resultadoPreEliminar.Municipio = await context.Municipios.SingleOrDefaultAsync(o => o.Id == dto.Municipio.Id);
            resultadoPreEliminar.MunicipioId = resultadoPreEliminar.Municipio.Id;

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
            resultadoPreEliminar.Comunidad = await context.Comunidades.SingleOrDefaultAsync(o => o.Id == dto.Comunidad.Id);
            resultadoPreEliminar.ComunidadId = resultadoPreEliminar.Comunidad.Id;

            context.Update(resultadoPreEliminar);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultadoPreEliminarExists(id))
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

        private bool ResultadoPreEliminarExists(int id)
        {
            return context.ResultadosPreEliminares.Any(e => e.Id == id);
        }

    }
}