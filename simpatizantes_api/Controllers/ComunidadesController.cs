using AutoMapper;
using simpatizantes_api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Filters;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/comunidades")]
    [ApiController]
    [TokenValidationFilter]

    public class ComunidadesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComunidadesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ComunidadDTO>>> GetAll()
        {
            string userName = User.FindFirst("NombreCompleto")?.Value;
            var estados = await context.Comunidades
                .Include(u => u.Municipio)
                .ToListAsync();

            if (!estados.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ComunidadDTO>>(estados));
        }

    }
}
