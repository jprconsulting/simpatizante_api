﻿using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Microsoft.AspNetCore.Authorization;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public OperadoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<OperadorDTO>> GetById(int id)
        {
            var operador = await context.Operadores
                .Include(o => o.OperadorSecciones)
                    .ThenInclude(os => os.Seccion)
                        .ThenInclude(s => s.Municipio)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (operador == null)
            {
                return NotFound();
            }

            var operadorDTO = mapper.Map<OperadorDTO>(operador);

            var secciones = operador.OperadorSecciones.Select(os => os.Seccion).ToList();
            operadorDTO.Secciones = mapper.Map<List<SeccionDTO>>(secciones);

            return Ok(operadorDTO);
        }


        [HttpGet("obtener-operadores-por-candidato-id/{candidatoId:int}")]
        public async Task<ActionResult<List<OperadorDTO>>> GetOperadoresPorCandidatoId(int candidatoId)
        {
            var operadores = await context.Operadores
                .Include(o => o.Candidato)
                .Include(o => o.OperadorSecciones)
                    .ThenInclude(os => os.Seccion)
                        .ThenInclude(s => s.Municipio)
                .Where(o => o.Candidato.Id == candidatoId)
                .ToListAsync();

            if (!operadores.Any())
            {
                return NotFound();
            }

            var operadoresDTO = mapper.Map<List<OperadorDTO>>(operadores);

            foreach (var operador in operadoresDTO)
            {
                var secciones = await context.OperadoresSecciones
                    .Include(s => s.Seccion)
                    .ThenInclude(s => s.Municipio)
                    .Where(os => os.Operador.Id == operador.Id)
                    .Select(i => i.Seccion)
                    .ToListAsync();

                operador.Secciones = mapper.Map<List<SeccionDTO>>(secciones);
            }

            return Ok(operadoresDTO);
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<OperadorDTO>>> GetAll()
        {
            var operadores = await context.Operadores
                .Include(o => o.Candidato)
                .ToListAsync();

            if (!operadores.Any())
            {
                return NotFound();
            }

            var operadoresDTO = mapper.Map<List<OperadorDTO>>(operadores);

            foreach (var operador in operadoresDTO)
            {
                var secciones = await context.OperadoresSecciones
                    .Include(s => s.Seccion)
                    .ThenInclude(s => s.Municipio)
                    .Where(os => os.Operador.Id == operador.Id)
                    .Select(i => i.Seccion)
                    .ToListAsync();

                operador.Secciones = mapper.Map<List<SeccionDTO>>(secciones);
            }

            return Ok(operadoresDTO);
        }


        [HttpPost("crear")]
        public async Task<ActionResult> Post(OperadorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existeOperador = await context.Operadores.AnyAsync(n => n.Nombres == dto.Nombres &&
                                                                  n.ApellidoPaterno == dto.ApellidoPaterno &&
                                                                  n.ApellidoMaterno == dto.ApellidoMaterno);
            if (existeOperador)
            {
                return Conflict();
            }
            string nombreCompleto = User.FindFirst("nombreCompleto")?.Value;

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var operador = mapper.Map<Operador>(dto);
                    operador.UsuarioCreacionNombre = nombreCompleto; // Establecer el UsuarioCreacionId
                    operador.FechaHoraCreacion = DateTime.Now; // Establecer la fecha de creación
                    operador.Candidato = await context.Candidatos.SingleOrDefaultAsync(r => r.Id == dto.Candidato.Id);
                    context.Add(operador);

                    if (await context.SaveChangesAsync() > 0)
                    {
                        foreach (var seccionId in dto.SeccionesIds)
                        {
                            var existsSeccion = await context.Secciones.SingleOrDefaultAsync(i => i.Id == seccionId);
                            var existsOperadorSeccion = await context.OperadoresSecciones.AnyAsync(os => os.Operador.Id == dto.Id && os.Seccion.Id == seccionId);

                            if (existsSeccion != null && !existsOperadorSeccion)
                            {
                                var operadorSeccion = new OperadorSeccion
                                {
                                    Operador = operador,
                                    Seccion = existsSeccion
                                };
                                context.Add(operadorSeccion);
                            }
                        }

                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return Ok();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { error = "Error interno del servidor al guardar el operador.", details = ex.Message });
                }
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var operador = await context.Operadores.FindAsync(id);

            if (operador == null)
            {
                return NotFound();
            }

            // Verificar si hay dependencias
            var tieneDependencias = await context.Simpatizantes.AnyAsync(os => os.OperadorId == id);

            if (tieneDependencias)
            {
                return StatusCode(502, new { error = "No se puede eliminar el operador debido a dependencias existentes." });
            }

            context.Operadores.Remove(operador);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, OperadorDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var operador = await context.Operadores
                .Include(o => o.OperadorSecciones)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (operador == null)
            {
                return NotFound();
            }

            // Actualizar propiedades de nombres y apellidos
            operador.CandidatoId = dto.Candidato.Id;
            operador.Nombres = dto.Nombres;
            operador.ApellidoPaterno = dto.ApellidoPaterno;
            operador.ApellidoMaterno = dto.ApellidoMaterno;
            operador.FechaNacimiento = dto.FechaNacimiento;
            operador.Estatus = dto.Estatus;

            // Limpiar todas las secciones actuales
            operador.OperadorSecciones.Clear();

            // Agregar las nuevas secciones
            foreach (var seccionId in dto.SeccionesIds)
            {
                var seccion = await context.Secciones.FindAsync(seccionId);
                if (seccion != null)
                {
                    operador.OperadorSecciones.Add(new OperadorSeccion { OperadorId = id, SeccionId = seccionId, Operador = operador, Seccion = seccion });
                }
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperadorExists(id))
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

        private bool OperadorExists(int id)
        {
            return context.Incidencias.Any(e => e.Id == id);
        }
    }
}
