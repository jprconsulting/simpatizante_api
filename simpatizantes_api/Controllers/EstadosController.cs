using AutoMapper;
using simpatizantes_api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace simpatizantes_api.Controllers
{
    [Authorize]
    [Route("api/estados")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EstadosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<EstadoDTO>>> GetAll()
        {
            string userName = User.FindFirst("NombreCompleto")?.Value;
            var estados = await context.Estados
                .ToListAsync();

            if (!estados.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<EstadoDTO>>(estados));
        }

    }
}
