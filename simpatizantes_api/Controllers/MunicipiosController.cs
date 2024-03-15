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
    [Route("api/municipios")]
    [ApiController]
    [TokenValidationFilter]

    public class MunicipiosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MunicipiosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<MunicipioDTO>> GetById(int id)
        {
            var municipios = await context.Municipios
                .Include(u => u.Estado)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (municipios == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<MunicipioDTO>(municipios));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<MunicipioDTO>>> GetAll()
        {
            var municipios = await context.Municipios
            .Include(u => u.Estado)
            .ToListAsync();
            if (!municipios.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<MunicipioDTO>>(municipios));
        }      

    }
}
