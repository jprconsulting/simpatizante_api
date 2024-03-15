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
    [Authorize]
    [Route("api/rols")]
    [ApiController]
    [TokenValidationFilter]

    public class RolsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RolsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<RolDTO>>> GetAll()
        {
            var rols = await context.Rols.ToListAsync();

            if (!rols.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<RolDTO>>(rols));
        }

    }
}
