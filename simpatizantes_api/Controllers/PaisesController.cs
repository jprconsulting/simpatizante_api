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

namespace simpatizantes_api.Controllers
{
    [Route("api/paises")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PaisesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<PaisDTO>>> GetAll()
        {
            var distritos = await context.Paises
            .ToListAsync();
            if (!distritos.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<PaisDTO>>(distritos));
        }

    }
}
