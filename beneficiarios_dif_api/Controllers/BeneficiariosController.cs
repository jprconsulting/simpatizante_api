using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Authorize]
    [Route("api/beneficiarios")]
    [ApiController]
    public class BeneficiariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BeneficiariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<BeneficiarioDTO>> GetById(int id)
        {
            var beneficiario = await context.Beneficiarios
                .Include(p => p.ProgramaSocial)
                .ThenInclude(a => a.AreaAdscripcion)
                .Include(m => m.Municipio)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (beneficiario == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BeneficiarioDTO>(beneficiario));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<BeneficiarioDTO>>> GetAll()
        {
            var beneficiarios = await context.Beneficiarios
                 .Include(p => p.ProgramaSocial)
                 .ThenInclude(a => a.AreaAdscripcion)
                 .Include(m => m.Municipio)
                 .ToListAsync();

            if (!beneficiarios.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<BeneficiarioDTO>>(beneficiarios));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(BeneficiarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existeBeneficiario = await context.Beneficiarios.AnyAsync(b => b.Nombres == dto.Nombres && b.ApellidoPaterno == dto.ApellidoPaterno);

            if (existeBeneficiario)
            {
                return Conflict();
            }

            var beneficiario = mapper.Map<Beneficiario>(dto);          
            beneficiario.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            beneficiario.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            context.Add(beneficiario);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beneficiario = await context.Beneficiarios.FindAsync(id);

            if (beneficiario == null)
            {
                return NotFound();
            }

            context.Beneficiarios.Remove(beneficiario);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, BeneficiarioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var beneficiario = await context.Beneficiarios.FindAsync(id);

            if (beneficiario == null)
            {
                return NotFound();
            }

            mapper.Map(dto, beneficiario);
            beneficiario.ProgramaSocial = await context.ProgramasSociales.SingleOrDefaultAsync(p => p.Id == dto.ProgramaSocial.Id);
            beneficiario.Municipio = await context.Municipios.SingleOrDefaultAsync(m => m.Id == dto.Municipio.Id);
            context.Update(beneficiario);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool BeneficiarioExists(int id)
        {
            return context.Beneficiarios.Any(e => e.Id == id);
        }

        [HttpGet("total-beneficiarios-por-municipio")]
        public async Task<ActionResult<List<TotalBeneficiariosMunicipioDTO>>> GetTotalBeneficiariosPorMunicipio()
        {
            try
            {
                var municipios = await context.Municipios.Include(m => m.Beneficiarios).ToListAsync();
                var indicadores = await context.Indicadores.ToListAsync();

                var municipiosDTO = municipios.Select(m =>
                {
                    var totalBeneficiarios = m.Beneficiarios.Count;
                    var indicador = indicadores.FirstOrDefault(i => totalBeneficiarios >= i.RangoInicial && totalBeneficiarios <= i.RangoFinal);
                    var color = indicador != null ? indicador.Color : "#FFFFFF";
                    var descripcionIndicador = indicador != null ? indicador.Descripcion : "Sin descripción";

                    return new TotalBeneficiariosMunicipioDTO
                    {
                        Id = m.Id,
                        Nombre = m.Nombre,
                        TotalBeneficiarios = totalBeneficiarios,
                        Color = color,
                        DescripcionIndicador = descripcionIndicador
                    };
                }).ToList();

                return Ok(municipiosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
