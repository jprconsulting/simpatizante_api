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
            var distribucionCandidatura = await context.distribucionescandidaturas
                .Include(u => u.TipoEleccion)
                .Include(u => u.Estado)
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
        [HttpGet("obtener-simpatizantes-por-candidato-id/{candidatoId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorCandidatoId(int candidatoId)
        {
            var simpatizantes = await context.simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .Include(c => c.Operador)
                .ThenInclude(c => c.Candidato)
                .Include(g => g.Genero)
                .Include(n => n.Promotor)
                .Where(s => s.Operador.Candidato.Id == candidatoId)
                .ToListAsync();

            if (!simpatizantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(simpatizantes));
        }

        [HttpGet("obtener-por-Tipo/{TipoEleccionId:int}")]
        public async Task<ActionResult<List<DistribucionCandidaturaDTO>>> GetByTipoId(int TipoEleccionId)
        {
            var distribucionCandidatura = await context.distribucionescandidaturas
                .Include(u => u.TipoEleccion)
                .Include(e => e.Estado)
                .Include(d => d.Distrito)
                .Include(m => m.Municipio)
                .Include(c => c.Comunidad)
                 .Where(s => s.TipoEleccion.Id == TipoEleccionId)
                 .ToListAsync();

            if (!distribucionCandidatura.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map <List<DistribucionCandidaturaDTO>>(distribucionCandidatura));
        }


        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<DistribucionCandidaturaDTO>>> GetAll()
        {
            var distribucionCandidatura = await context.distribucionescandidaturas
                .Include(u => u.TipoEleccion)
                .Include(u => u.Estado)
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
                distribucionCandidatura.TipoEleccion = await context.tiposelecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);
                distribucionCandidatura.DistritoId = null;
                distribucionCandidatura.MunicipioId = null;
                distribucionCandidatura.ComunidadId = null;
                distribucionCandidatura.Distrito = null;
                distribucionCandidatura.Municipio = null;
                distribucionCandidatura.Comunidad = null;
                distribucionCandidatura.Estado = null;
                distribucionCandidatura.EstadoId = null;

                if (dto.TipoEleccion.Id == 7)
                {
                    var existeEstado = await context.distribucionescandidaturas
                                                .AnyAsync(c => c.TipoEleccion.Id == dto.TipoEleccion.Id);
                    if (existeEstado)
                    {
                        return Conflict();
                    }
                    distribucionCandidatura.Estado = await context.estados.SingleOrDefaultAsync(c => c.Id == dto.Estado.Id);
                }

                if (dto.TipoEleccion.Id == 8)
                {
                    var existeEstado = await context.distribucionescandidaturas
                                                .AnyAsync(c => c.TipoEleccion.Id == dto.TipoEleccion.Id);
                    if (existeEstado)
                    {
                        return Conflict();
                    }
                    distribucionCandidatura.Estado = await context.estados.SingleOrDefaultAsync(c => c.Id == dto.Estado.Id);
                }
                if (dto.TipoEleccion.Id == 9)
                {
                    var existeEstado = await context.distribucionescandidaturas
                                                .AnyAsync(c => c.TipoEleccion.Id == dto.TipoEleccion.Id);
                    if (existeEstado)
                    {
                        return Conflict();
                    }
                    distribucionCandidatura.Estado = await context.estados.SingleOrDefaultAsync(c => c.Id == dto.Estado.Id);
                }
                // Si es  Diputacion Local
                if (dto.TipoEleccion.Id == 10)
                {
                    var existe = await context.distribucionescandidaturas
                                                .AnyAsync(c => c.Distrito.Id == dto.Distrito.Id);
                    if (existe)
                    {
                        return Conflict();
                    }
                    distribucionCandidatura.Distrito = await context.distritos.SingleOrDefaultAsync(c => c.Id == dto.Distrito.Id);
                }

                // Si es  Ayuntamientos
                if (dto.TipoEleccion.Id == 11)
                {
                    var existe = await context.distribucionescandidaturas
                                                .AnyAsync(c => c.Municipio.Id == dto.Municipio.Id);
                    if (existe)
                    {
                        return Conflict();
                    }
                    distribucionCandidatura.Municipio = await context.municipios.SingleOrDefaultAsync(c => c.Id == dto.Municipio.Id);
                }

                // Si es Comunidad
                if (dto.TipoEleccion.Id == 12)
                {
                    var existe = await context.distribucionescandidaturas
                                                .AnyAsync(c => c.Comunidad.Id == dto.Comunidad.Id);
                    if (existe)
                    {
                        return Conflict();
                    }
                    distribucionCandidatura.Comunidad = await context.comunidades.SingleOrDefaultAsync(c => c.Id == dto.Comunidad.Id);
                    if (dto.Partidos != null && dto.Partidos.Count % 1 == 0)
                    {
                        distribucionCandidatura.Partidos = string.Join(",", dto.Partidos);
                    }
                    if (dto.Coalicion != null && dto.Coalicion.Count % 1 == 0)
                    {
                        distribucionCandidatura.Coalicion = string.Join(",", dto.Coalicion);
                    }
                    if (dto.Comun != null && dto.Comun.Count % 1 == 0)
                    {
                        distribucionCandidatura.Comun = string.Join(",", dto.Comun);
                    }
                    if (dto.Independiente != null && dto.Independiente.Count % 1 == 0)
                    {
                        distribucionCandidatura.Independiente = string.Join(",", dto.Independiente);
                    }

                }

                else
                {
                    
                    if (dto.Partidos != null && dto.Partidos.Count % 1 == 0)
                    {
                        distribucionCandidatura.Partidos = string.Join(",", dto.Partidos);
                    }
                    if (dto.Coalicion != null && dto.Coalicion.Count % 1 == 0)
                    {
                        distribucionCandidatura.Coalicion = string.Join(",", dto.Coalicion);
                    }
                    if (dto.Comun != null && dto.Comun.Count % 1 == 0)
                    {
                        distribucionCandidatura.Comun = string.Join(",", dto.Comun);
                    }
                    if (dto.Independiente != null && dto.Independiente.Count % 1 == 0)
                    {
                        distribucionCandidatura.Independiente = string.Join(",", dto.Independiente);
                    }
                   
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
            var distribucionCandidatura = await context.distribucionescandidaturas.FindAsync(id);

            if (distribucionCandidatura == null)
            {
                return NotFound();
            }

            context.distribucionescandidaturas.Remove(distribucionCandidatura);
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

            var distribucionCandidatura = await context.distribucionescandidaturas.FindAsync(id);
            var currentEstadoId = distribucionCandidatura?.EstadoId;
            var currentDistritoId = distribucionCandidatura?.DistritoId;
            var currentMunicipioId = distribucionCandidatura?.MunicipioId;
            var currentComunidadId = distribucionCandidatura?.ComunidadId;

            if (distribucionCandidatura == null)
            {
                return NotFound();
            }

            // Mapea los datos del DTO al usuario existente
            mapper.Map(dto, distribucionCandidatura);
            distribucionCandidatura.TipoEleccion = await context.tiposelecciones.SingleOrDefaultAsync(r => r.Id == dto.TipoEleccion.Id);

            distribucionCandidatura.Distrito = null;
            distribucionCandidatura.DistritoId = null;
            distribucionCandidatura.Municipio = null;
            distribucionCandidatura.MunicipioId = null;
            distribucionCandidatura.Comunidad = null;
            distribucionCandidatura.ComunidadId = null;
            distribucionCandidatura.Estado = null;
            distribucionCandidatura.EstadoId = null;

            // Si es  Gubernatura
            if (dto.TipoEleccion.Id == 7)
            {
                if (currentEstadoId != null && currentEstadoId != dto.Estado.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Estado.Id == dto.Estado.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }

                distribucionCandidatura.Estado = await context.estados.SingleOrDefaultAsync(o => o.Id == dto.Estado.Id);
                distribucionCandidatura.EstadoId = distribucionCandidatura.Estado.Id;
            }

            // Si es  Diputados Locales
            if (dto.TipoEleccion.Id == 8)
            {
                if (currentEstadoId != null && currentEstadoId != dto.Estado.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Estado.Id == dto.Estado.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
                distribucionCandidatura.Estado = await context.estados.SingleOrDefaultAsync(o => o.Id == dto.Estado.Id);
                distribucionCandidatura.EstadoId = distribucionCandidatura.Estado.Id;
            }

            if (dto.TipoEleccion.Id == 9)
            {
                if (currentEstadoId != null && currentEstadoId != dto.Estado.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Estado.Id == dto.Estado.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
                distribucionCandidatura.Estado = await context.estados.SingleOrDefaultAsync(o => o.Id == dto.Estado.Id);
                distribucionCandidatura.EstadoId = distribucionCandidatura.Estado.Id;
            }

            // Si es  Ayuntamientos
            if (dto.TipoEleccion.Id == 10)
            {
                if (currentDistritoId != null && currentDistritoId != dto.Distrito.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Distrito.Id == dto.Distrito.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
                distribucionCandidatura.Distrito = await context.distritos.SingleOrDefaultAsync(o => o.Id == dto.Distrito.Id);
                distribucionCandidatura.DistritoId = distribucionCandidatura.Distrito.Id;
            }

            if (dto.TipoEleccion.Id == 11)
            {
                if (currentMunicipioId != null && currentMunicipioId != dto.Municipio.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Municipio.Id == dto.Municipio.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }
                distribucionCandidatura.Municipio = await context.municipios.SingleOrDefaultAsync(o => o.Id == dto.Municipio.Id);
                distribucionCandidatura.MunicipioId = distribucionCandidatura.Municipio.Id;
            }


            // Si es  Comunidad
            if (dto.TipoEleccion.Id == 12)
            {
                if (currentComunidadId != null && currentComunidadId != dto.Comunidad.Id)
                {
                    var existsUserOperador = await context.distribucionescandidaturas.AnyAsync(c => c.Comunidad.Id == dto.Comunidad.Id);
                    if (existsUserOperador)
                    {
                        return Conflict();
                    }
                }distribucionCandidatura.Comunidad = await context.comunidades.SingleOrDefaultAsync(o => o.Id == dto.Comunidad.Id);
            distribucionCandidatura.ComunidadId = distribucionCandidatura.Comunidad.Id;
                distribucionCandidatura.Partidos = string.Join(",", dto.Partidos);
                distribucionCandidatura.Coalicion = string.Join(",", dto.Coalicion);
                distribucionCandidatura.Comun = string.Join(",", dto.Comun);
                distribucionCandidatura.Independiente = string.Join(",", dto.Independiente);
            }
            else
            {

                if (dto.Partidos != null && dto.Partidos.Count % 1 == 0)
                {
                    distribucionCandidatura.Partidos = string.Join(",", dto.Partidos);
                }
                if (dto.Coalicion != null && dto.Coalicion.Count % 1 == 0)
                {
                    distribucionCandidatura.Coalicion = string.Join(",", dto.Coalicion);
                }
                if (dto.Comun != null && dto.Comun.Count % 1 == 0)
                {
                    distribucionCandidatura.Comun = string.Join(",", dto.Comun);
                }
                if (dto.Independiente != null && dto.Independiente.Count % 1 == 0)
                {
                    distribucionCandidatura.Independiente = string.Join(",", dto.Independiente);
                }

            }



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
            return context.distribucionescandidaturas.Any(e => e.Id == id);
        }

    }     
}