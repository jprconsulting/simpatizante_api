using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using simpatizantes_api.Filters;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/secciones")]
    [ApiController]
    [TokenValidationFilter]

    public class SeccionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SeccionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<SeccionDTO>>> GetAll()
        {
            var secciones = await context.secciones
            .Include(u => u.Municipio)
            .ToListAsync();
            if (!secciones.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SeccionDTO>>(secciones));
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<SeccionDTO>> GetById(int id)
        {
            var usuario = await context.secciones
                .Include(u => u.Municipio)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SeccionDTO>(usuario));
        }

        [HttpGet("por-municipio/{municipioId}")]
        public async Task<ActionResult<List<SeccionDTO>>> GetByMunicipio(int municipioId)
        {
            var secciones = await context.secciones
                .Include(u => u.Municipio)
                .Where(s => s.Municipio.Id == municipioId)
                .ToListAsync();

            if (!secciones.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SeccionDTO>>(secciones));
        }
    }
}
