using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/indicadores")]
    [ApiController]
    public class IndicadoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public IndicadoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<IndicadorDTO>>> GetAll()
        {
            var indicadores = await context.Indicadores.ToListAsync();

            if (!indicadores.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<IndicadorDTO>>(indicadores));
        }
        private bool IndicadorExists(int id)
        {
            return context.Indicadores.Any(e => e.Id == id);
        }
    }
}
