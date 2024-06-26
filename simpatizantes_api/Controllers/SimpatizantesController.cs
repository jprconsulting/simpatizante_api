﻿using AutoMapper;
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
    [Route("api/simpatizantes")]
    [ApiController]
    [TokenValidationFilter]

    public class SimpatizantesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SimpatizantesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private async Task<bool> ValidarSimpatizantePorCURP(string curp)
        {
            if (curp == null)
            {
                return false;
            }

            return await context.simpatizantes.AnyAsync(s => s.CURP.Trim().ToLower() == curp.Trim().ToLower());
        }


        private async Task<bool> ValidarSimpatizantePorClaveElector(string claveElector)
        {
            return await context.simpatizantes.AnyAsync(s => s.ClaveElector.Trim().ToLower() == claveElector.Trim().ToLower());
        }

        [HttpGet("validar-simpatizante-por-clave-elector/{claveElector}")]
        public async Task<ActionResult> GetValidarSimpatizantePorClaveElector(string claveElector)
        {
            var existeSimpatizante = await ValidarSimpatizantePorClaveElector(claveElector);

            if (existeSimpatizante)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("validar-simpatizante-por-curp/{curp}")]
        public async Task<ActionResult> GetValidarSimpatizantePorCURP(string curp)
        {
            var existeSimpatizante = await ValidarSimpatizantePorCURP(curp);

            if (existeSimpatizante)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<SimpatizanteDTO>> GetById(int id)
        {
            var simpatizante = await context.simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(n => n.Promotor)
                .Include(p => p.ProgramaSocial)
                .Include(c => c.Operador)
                .Include(g => g.Genero)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (simpatizante == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SimpatizanteDTO>(simpatizante));
        }   

        [HttpGet("obtener-simpatizantes-por-operador-id/{operadorId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorOperadorId(int operadorId)
        {
            var simpatizantes = await context.simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(n => n.Promotor)
                .Include(p => p.ProgramaSocial)
                .Include(c => c.Operador)
                .ThenInclude(c => c.Candidato)
                .Include(g => g.Genero)
                .Where(s => s.Operador.Id == operadorId)
                .ToListAsync();

            if (!simpatizantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(simpatizantes));
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

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetAll()
        {
            var simpatizantes = await context.simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(o => o.Operador)
                .ThenInclude(c => c.Candidato)
                .Include(n => n.Promotor)
                .Include(p => p.ProgramaSocial)
                .Include(g => g.Genero)
                .OrderByDescending(i => i.Id)
                .ToListAsync();

            if (!simpatizantes.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SimpatizanteDTO>>(simpatizantes));
        }

        [HttpGet("mapa")]
        public async Task<ActionResult<List<SimpatizanteConVisitaDTO>>> GetSimpatizantesConSimpatiza()
        {
            var visitas = await context.visitas.
                ToListAsync(); 

            var simpatizantes = await context.simpatizantes
               .Include(s => s.Seccion)
               .Include(m => m.Municipio)
               .Include(e => e.Estado)
               .Include(o => o.Operador)
               .ThenInclude(c => c.Candidato)
               .Include(n => n.Promotor)
               .Include(p => p.ProgramaSocial)
               .Include(g => g.Genero)
               .OrderByDescending(i => i.Id)
               .ToListAsync();

            var simpatizantesConVisita = visitas.Select(v => new SimpatizanteConVisitaDTO
            {
                Simpatizante = mapper.Map<SimpatizanteDTO>(v.Simpatizante),
                Simpatiza = v.Simpatiza,
                Color = GetColorFromSimpatiza(v.Simpatiza)
            }).ToList();

            return Ok(simpatizantesConVisita);
        }

        [HttpGet("obtener-simpatizantessimpatiza-por-candidato-id/{candidatoId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesConSimpatizaCId(int candidatoId)
        {
            var visitas = await context.visitas.
               ToListAsync();

            var simpatizantes = await context.simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(p => p.ProgramaSocial)
                .Include(c => c.Operador)
                .Include(g => g.Genero)
                .Include(n => n.Promotor)
                .Where(s => s.Operador.Candidato.Id == candidatoId)
                .ToListAsync();

            var simpatizantesConVisita2 = visitas.Select(v => new SimpatizanteConVisitaDTO
            {
                Simpatizante = mapper.Map<SimpatizanteDTO>(v.Simpatizante),
                Simpatiza = v.Simpatiza,
                Color = GetColorFromSimpatiza(v.Simpatiza)
            }).ToList();

            return Ok(simpatizantesConVisita2);            
        }
        [HttpGet("obtener-simpatizantes-por-simpatiza-operador-id/{operadorId:int}")]
        public async Task<ActionResult<List<SimpatizanteDTO>>> GetSimpatizantesPorSimpatizaOperadorId(int operadorId)
        {
            var visitas = await context.visitas.
               ToListAsync();
            var simpatizantes = await context.simpatizantes
                .Include(s => s.Seccion)
                .Include(m => m.Municipio)
                .Include(e => e.Estado)
                .Include(n => n.Promotor)
                .Include(p => p.ProgramaSocial)
                .Include(c => c.Operador)
                .Include(g => g.Genero)
                .Where(s => s.Operador.Id == operadorId)
                .ToListAsync();

            var simpatizantesConVisita3 = visitas.Select(v => new SimpatizanteConVisitaDTO
            {
                Simpatizante = mapper.Map<SimpatizanteDTO>(v.Simpatizante),
                Simpatiza = v.Simpatiza,
                Color = GetColorFromSimpatiza(v.Simpatiza)
            }).ToList();

            return Ok(simpatizantesConVisita3);

           
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(SimpatizanteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeSimpatizante = await ValidarSimpatizantePorCURP(dto.CURP);

            if (existeSimpatizante)
            {
                return Conflict();
            }
            var existeSimpatizantern = await context.simpatizantes.AnyAsync(n => n.Nombres == dto.Nombres &&
                                                                n.ApellidoPaterno == dto.ApellidoPaterno &&
                                                                n.ApellidoMaterno == dto.ApellidoMaterno);
            if (existeSimpatizantern)
            {
                return Conflict();
            }

            string nombreCompleto = User.FindFirst("nombreCompleto")?.Value;

            var simpatizante = mapper.Map<Simpatizante>(dto);

            simpatizante.UsuarioCreacionNombre = nombreCompleto; // Establecer el UsuarioCreacionId
            simpatizante.FechaHoraCreacion = DateTime.Now; // Establecer la fecha de creación

            simpatizante.Seccion = await context.secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            simpatizante.Municipio = await context.municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            simpatizante.Estado = await context.estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);
            simpatizante.Operador = await context.operadores.SingleOrDefaultAsync(r => r.Id == dto.Operador.Id);
            simpatizante.Genero = await context.generos.SingleOrDefaultAsync(g => g.Id == dto.Genero.Id);

            if (dto.ProgramaSocial != null)
            {
                simpatizante.ProgramaSocial = await context.programassociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            }

            if (dto.Promotor != null)
            {
                simpatizante.Promotor = await context.promotores.SingleOrDefaultAsync(p => p.Id == dto.Promotor.Id);
            }

            context.Add(simpatizante);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar el Simpatizante.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var simpatizante = await context.simpatizantes.FindAsync(id);

            if (simpatizante == null)
            {
                return NotFound();
            }

            var tieneDependencias = await context.visitas.AnyAsync(s => s.SimpatizanteId == id);

            if (tieneDependencias)
            {
                return StatusCode(502, new { error = "No se puede eliminar el simpatizante debido a dependencias existentes." });
            }

            context.simpatizantes.Remove(simpatizante);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, SimpatizanteDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var simpatizante = await context.simpatizantes.FindAsync(id);

            if (simpatizante == null)
            {
                return NotFound();
            }

            string nombreCompleto = User.FindFirst("nombreCompleto")?.Value;

            if (dto.FechaNacimiento == null)
            {
                dto.FechaNacimiento = DateTime.Now;
            }
            
            // Mapear el DTO actualizado al simpatizante
            mapper.Map(dto, simpatizante);

            simpatizante.UsuarioEdicionNombre = nombreCompleto; // Establecer el UsuarioEdicionId
            simpatizante.FechaHoraEdicion = DateTime.Now; // Establecer la fecha de creación

            simpatizante.UsuarioCreacionNombre = nombreCompleto; 
            simpatizante.FechaHoraCreacion = DateTime.Now; 
            simpatizante.Seccion = await context.secciones.SingleOrDefaultAsync(s => s.Id == dto.Seccion.Id);
            simpatizante.Municipio = await context.municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            simpatizante.Estado = await context.estados.SingleOrDefaultAsync(e => e.Id == dto.Estado.Id);
            simpatizante.Operador = await context.operadores.SingleOrDefaultAsync(r => r.Id == dto.Operador.Id);
            simpatizante.Genero = await context.generos.SingleOrDefaultAsync(g => g.Id == dto.Genero.Id);

            if (dto.ProgramaSocial != null)
            {
                simpatizante.ProgramaSocial = await context.programassociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            }
             
            if (dto.Promotor != null)
            {
                simpatizante.Promotor = await context.promotores.SingleOrDefaultAsync(p => p.Id == dto.Promotor.Id);
            }

            // Actualizar el simpatizante en la base de datos
            context.Update(simpatizante);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimpatizanteExists(id))
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
        private string GetColorFromSimpatiza(bool simpatiza)
        {
            return simpatiza ? "#008f39 " : "#FF0000";
        }

        private bool SimpatizanteExists(int id)
        {
            return context.simpatizantes.Any(s => s.Id == id);
        }

    }
}