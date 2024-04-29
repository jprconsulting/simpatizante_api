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
            var resultadoPreEliminar = await context.resultadospreeliminares
                .Include(u => u.TipoEleccion)
                .Include(u => u.Estado)
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
            var resultadoPreEliminar = await context.resultadospreeliminares
                .Include(u => u.TipoEleccion)
                .Include(u => u.Estado)
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
                resultadoPreEliminar.TipoEleccion = await context.tiposelecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);
                resultadoPreEliminar.Seccion = await context.secciones.SingleOrDefaultAsync(r => r.Id == dto.Seccion.Id);
                resultadoPreEliminar.Casilla = await context.casillas.SingleOrDefaultAsync(r => r.Id == dto.Casilla.Id);
                resultadoPreEliminar.DistritoId = null;
                resultadoPreEliminar.MunicipioId = null;
                resultadoPreEliminar.ComunidadId = null;
                resultadoPreEliminar.Distrito = null;
                resultadoPreEliminar.Municipio = null;
                resultadoPreEliminar.Comunidad = null;
                resultadoPreEliminar.Estado = null;
                resultadoPreEliminar.EstadoId = null;
                // Si es  Gubernatura
                if (dto.TipoEleccion.Id == 7)
                {
                    var existeEstado = await context.resultadospreeliminares
                                                .AnyAsync(c => c.TipoEleccion.Id == dto.TipoEleccion.Id);
                    if (existeEstado)
                    {
                        return Conflict();
                    }
                    resultadoPreEliminar.Estado = await context.estados.SingleOrDefaultAsync(c => c.Id == dto.Estado.Id);
                }

                // Si es  Diputacion Local
                if (dto.TipoEleccion.Id == 8)
                {
                    var existeEstado = await context.resultadospreeliminares
                                                .AnyAsync(c => c.TipoEleccion.Id == dto.TipoEleccion.Id);
                    if (existeEstado)
                    {
                        return Conflict();
                    }
                    resultadoPreEliminar.Estado = await context.estados.SingleOrDefaultAsync(c => c.Id == dto.Estado.Id);
                }

                // Si es  Ayuntamientos
                if (dto.TipoEleccion.Id == 9)
                {
                    var existeEstado = await context.resultadospreeliminares
                                                .AnyAsync(c => c.TipoEleccion.Id == dto.TipoEleccion.Id);
                    if (existeEstado)
                    {
                        return Conflict();
                    }
                    resultadoPreEliminar.Estado = await context.estados.SingleOrDefaultAsync(c => c.Id == dto.Estado.Id);
                }

                // Si es Comunidad
                if (dto.TipoEleccion.Id == 10)
                {
                    var existe = await context.resultadospreeliminares
                                                .AnyAsync(c => c.Distrito.Id == dto.Distrito.Id);
                    if (existe)
                    {
                        return Conflict();
                    }
                    resultadoPreEliminar.Distrito = await context.distritos.SingleOrDefaultAsync(c => c.Id == dto.Distrito.Id);

                    resultadoPreEliminar.Partidos = string.Join(",", dto.Partidos.Select(p => p.ToString()));
                }
                if (dto.TipoEleccion.Id == 11)
                {
                    var existe = await context.resultadospreeliminares
                                                .AnyAsync(c => c.Municipio.Id == dto.Municipio.Id);
                    if (existe)
                    {
                        return Conflict();
                    }
                    resultadoPreEliminar.Municipio = await context.municipios.SingleOrDefaultAsync(c => c.Id == dto.Municipio.Id);
                }

                // Si es Comunidad
                if (dto.TipoEleccion.Id == 12)
                {
                    var existe = await context.resultadospreeliminares
                                                .AnyAsync(c => c.Comunidad.Id == dto.Comunidad.Id);
                    if (existe)
                    {
                        return Conflict();
                    }
                    resultadoPreEliminar.Comunidad = await context.comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);

                }

                else
                {
                    if (dto.Partidos != null && dto.Partidos.Count % 1 == 0)
                    {
                        resultadoPreEliminar.Partidos = string.Join(",", dto.Partidos);
                    }
                    
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
            var resultadoPreEliminar = await context.resultadospreeliminares.FindAsync(id);

            if (resultadoPreEliminar == null)
            {
                return NotFound();
            }

            context.resultadospreeliminares.Remove(resultadoPreEliminar);
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

            var resultadoPreEliminar = await context.resultadospreeliminares.FindAsync(id);

            var currentDistritoId = resultadoPreEliminar?.DistritoId;
            var currentMunicipioId = resultadoPreEliminar?.MunicipioId;
            var currentComunidadId = resultadoPreEliminar?.ComunidadId;
            var currentEstadoId = resultadoPreEliminar?.EstadoId;
            if (resultadoPreEliminar == null)
            {
                return NotFound();
            }

            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, resultadoPreEliminar);
            resultadoPreEliminar.TipoEleccion = await context.tiposelecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);
            resultadoPreEliminar.Seccion = await context.secciones.SingleOrDefaultAsync(r => r.Id == dto.Seccion.Id);
            resultadoPreEliminar.Casilla = await context.casillas.SingleOrDefaultAsync(r => r.Id == dto.Casilla.Id);

            resultadoPreEliminar.Estado = null;
            resultadoPreEliminar.EstadoId = null;
            resultadoPreEliminar.Distrito = null;
            resultadoPreEliminar.DistritoId = null;
            resultadoPreEliminar.Municipio = null;
            resultadoPreEliminar.MunicipioId = null;
            resultadoPreEliminar.Comunidad = null;
            resultadoPreEliminar.ComunidadId = null;

            // Si es  Gubernatura
            if (dto.TipoEleccion.Id == 1)
            {
                if (currentEstadoId != null && currentEstadoId != dto.Estado.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Estado.Id == dto.Estado.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }

                resultadoPreEliminar.Estado = await context.estados.SingleOrDefaultAsync(o => o.Id == dto.Estado.Id);
                resultadoPreEliminar.EstadoId = resultadoPreEliminar.Estado.Id;
            }

            // Si es  Diputados Locales
            if (dto.TipoEleccion.Id == 2)
            {
                if (currentEstadoId != null && currentEstadoId != dto.Estado.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Estado.Id == dto.Estado.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
                resultadoPreEliminar.Estado = await context.estados.SingleOrDefaultAsync(o => o.Id == dto.Estado.Id);
                resultadoPreEliminar.EstadoId = resultadoPreEliminar.Estado.Id;
            }

            if (dto.TipoEleccion.Id == 3)
            {
                if (currentEstadoId != null && currentEstadoId != dto.Estado.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Estado.Id == dto.Estado.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
                resultadoPreEliminar.Estado = await context.estados.SingleOrDefaultAsync(o => o.Id == dto.Estado.Id);
                resultadoPreEliminar.EstadoId = resultadoPreEliminar.Estado.Id;
            }

            // Si es  Ayuntamientos
            if (dto.TipoEleccion.Id == 4)
            {
                if (currentDistritoId != null && currentDistritoId != dto.Distrito.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Distrito.Id == dto.Distrito.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
                resultadoPreEliminar.Distrito = await context.distritos.SingleOrDefaultAsync(o => o.Id == dto.Distrito.Id);
                resultadoPreEliminar.DistritoId = resultadoPreEliminar.Distrito.Id;
            }

            if (dto.TipoEleccion.Id == 5)
            {
                if (currentMunicipioId != null && currentMunicipioId != dto.Municipio.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Municipio.Id == dto.Municipio.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
                resultadoPreEliminar.Municipio = await context.municipios.SingleOrDefaultAsync(o => o.Id == dto.Municipio.Id);
                resultadoPreEliminar.MunicipioId = resultadoPreEliminar.Municipio.Id;
            }


            // Si es  Comunidad
            if (dto.TipoEleccion.Id == 6)
            {
                if (currentComunidadId != null && currentComunidadId != dto.Comunidad.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Comunidad.Id == dto.Comunidad.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }

                resultadoPreEliminar.Comunidad = await context.comunidades.SingleOrDefaultAsync(o => o.Id == dto.Comunidad.Id);
                resultadoPreEliminar.ComunidadId = resultadoPreEliminar.Comunidad.Id;
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
            return context.resultadospreeliminares.Any(e => e.Id == id);
        }

    }
}