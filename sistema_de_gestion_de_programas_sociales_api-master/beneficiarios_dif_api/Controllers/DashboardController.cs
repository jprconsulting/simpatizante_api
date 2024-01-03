using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace beneficiarios_dif_api.Controllers
{
    [Authorize]
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DashboardController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("total-beneficiarios-por-programa-social")]
        public async Task<ActionResult<List<ProgramaSocialEstadisticaDTO>>> TotalBeneficiariosPorProgramaSocial()
        {
            var beneficiarios = await context.Beneficiarios.ToListAsync();
            var totalBeneficiarios = beneficiarios.Count;

            if (totalBeneficiarios == 0)
            {
                return Ok(new List<ProgramaSocialEstadisticaDTO>());
            }

            var programasSociales = await context.ProgramasSociales
                .Include(p => p.Beneficiarios).ToListAsync();

            var estadisticas = programasSociales
                .Select(programa => new ProgramaSocialEstadisticaDTO
                {
                    Id = programa.Id,
                    Nombre = programa.Nombre,
                    Color = programa.Color,
                    TotalBeneficiarios = programa.Beneficiarios?.Count ?? 0,
                    Porcentaje = programa.Beneficiarios?.Count * 100 / totalBeneficiarios ?? 0
                })
                .ToList();

            return Ok(estadisticas);
        }

        [HttpGet("total-visitas-por-programa-social")]
        public async Task<ActionResult<List<EstadisticaDTO>>> TotalVisitasPorProgramaSocial()
        {
            var resultados = await context.ProgramasSociales
                .Include(p => p.Beneficiarios)
                    .ThenInclude(b => b.Visitas)
                .Where(p => p.Estatus)
                .Select(p => new EstadisticaDTO
                {
                    Nombre = p.Nombre,
                    Total = p.Beneficiarios.SelectMany(b => b.Visitas).Count()
                })
                .ToListAsync();

            return Ok(resultados);
        }

        [HttpGet("total-beneficiarios-por-municipio")]
        public async Task<ActionResult<List<EstadisticaDTO>>> TotalBeneficiariosPorMunicipio()
        {
            var resultados = await context.Municipios
                .Include(m => m.Beneficiarios)
                .Select(p => new EstadisticaDTO
                {
                    Nombre = p.Nombre,
                    Total = p.Beneficiarios.Count,
                })
                .ToListAsync();

            return Ok(resultados);
        }

        [HttpGet("total-general")]
        public async Task<ActionResult<TotalGeneralDTO>> TotalGeneral()
        {
            var totales = new TotalGeneralDTO()
            {
                TotalBeneficiarios = (await context.Beneficiarios.ToListAsync()).Count,
                TotalProgramasSociales = (await context.ProgramasSociales.ToListAsync()).Count,
                TotalUsuarios = (await context.Usuarios.ToListAsync()).Count,
                TotalVisitas = (await context.Visitas.ToListAsync()).Count,
            };

            return Ok(totales);
        }

        [HttpGet("obtener-nube-palabras")]
        public async Task<ActionResult> WordCloud()
        {
            var comments = await context.Visitas.Select(v => v.Descripcion).ToListAsync();
            var wordCount = CountWords(comments);

            var generalWordCloud = new GeneralWordCloudDTO
            {
                WordCloudPorMunicipios = new List<MunicipioWordCloudDTO>(),
                GeneralWordCloud = CreateModel(wordCount)
            };

            var municipios = await context.Municipios.ToListAsync();

            foreach (var municipio in municipios)
            {
                var visitasPorMunicipio = await context.Visitas
                 .Include(v => v.Beneficiario)
                    .ThenInclude(b => b.Municipio)
                 .Where(v => v.Beneficiario.Municipio.Id == municipio.Id)
                 .ToListAsync();

                var commentsByMunicipio = visitasPorMunicipio.Select(v => v.Descripcion).ToList();
                var wordCountByMunicipio = CountWords(commentsByMunicipio);
                var municipioWordCloud = new MunicipioWordCloudDTO
                {
                    Id = municipio.Id,
                    Nombre = municipio.Nombre,
                    WordCloud = CreateModel(wordCountByMunicipio)
                };
                generalWordCloud.WordCloudPorMunicipios.Add(municipioWordCloud);
            }

            return Ok(generalWordCloud);
        }

        static Dictionary<string, int> CountWords(List<string> comments)
        {
            var words = comments.SelectMany(c => Regex.Matches(c.ToLower(), @"\b\w+\b").Select(match => match.Value)).ToList();

            var wordCount = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount[word] = 1;
                }
            }

            return wordCount;
        }

        static List<WordCloudDTO> CreateModel(Dictionary<string, int> wordCount)
        {
            return wordCount
                .Select(pair => new WordCloudDTO { Name = pair.Key, Weight = pair.Value })
                .OrderByDescending(modelo => modelo.Weight)
                .ToList();
        }
    }
}
