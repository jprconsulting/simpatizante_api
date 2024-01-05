using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/rols")]
    [ApiController]
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
