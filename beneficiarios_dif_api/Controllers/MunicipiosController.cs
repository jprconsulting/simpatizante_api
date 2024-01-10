using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/municipios")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MunicipiosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
