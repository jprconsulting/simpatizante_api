using AutoMapper;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Filters;

namespace simpatizantes_api.Controllers
{
    [Route("api/genero")]
    [ApiController]

    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GeneroController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<GeneroDTO>>> GetAll()
        {
            var rols = await context.generos.ToListAsync();

            if (!rols.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<GeneroDTO>>(rols));
        }

    }
}
