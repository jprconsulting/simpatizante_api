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
    [Route("api/secciones")]
    [ApiController]
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
            var secciones = await context.Secciones
            .Include(u => u.Municipio)
            .ToListAsync();
            if (!secciones.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SeccionDTO>>(secciones));
        }

    }
}
