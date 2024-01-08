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
    [Route("api/programas")]
    [ApiController]
    public class ProgramasSocialesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProgramasSocialesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ProgramaSocialDTO>>> GetAll()
        {
            var ProgramasSociales = await context.ProgramasSociales.ToListAsync();

            if (!ProgramasSociales.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ProgramaSocialDTO>>(ProgramasSociales));
        }

    }
}
