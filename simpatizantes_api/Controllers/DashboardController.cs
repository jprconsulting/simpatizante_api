﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using System.Text.RegularExpressions;

namespace simpatizantes_api.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DashboardController(ApplicationDbContext context)
        {
            this.context = context;
        }      

        [HttpGet("obtener-nube-palabras")]
        public async Task<ActionResult> WordCloud()
        {
            var comments = await context.Visitas.Select(v => v.Servicio).ToListAsync();
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
                 .Include(v => v.Simpatizante)
                    .ThenInclude(b => b.Municipio)
                 .Where(v => v.Simpatizante.Municipio.Id == municipio.Id)
                 .ToListAsync();

                var commentsByMunicipio = visitasPorMunicipio.Select(v => v.Servicio).ToList();
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
