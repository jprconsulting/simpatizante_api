﻿using AutoMapper;
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
    [Route("api/distritos")]
    [ApiController]
    public class DistritosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DistritosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<MunicipioDTO>>> GetAll()
        {
            var distritos = await context.Distritos            
            .ToListAsync();
            if (!distritos.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<DistritoDTO>>(distritos));
        }

    }
}
