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
    [Route("api/localidades")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LocalidadController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<LocalidadDTO>>> GetAll()
        {
            var localidades = await context.Localidades.ToListAsync();

            if (!localidades.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<LocalidadDTO>>(localidades));
        }

    }
}
