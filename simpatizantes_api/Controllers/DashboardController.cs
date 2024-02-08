
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;


namespace simpatizantes_api.Controllers
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
        [HttpGet("total-Simpatizantes-por-programa-social")]
        public async Task<ActionResult<List<SimpatizantesEstadisticaDTO>>> TotalSimpatizantesPorProgramaSocial()
        {
            var Simpatizantes = await context.Simpatizantes.ToListAsync();
            var totalSimpatizantes = Simpatizantes.Count;

            if (totalSimpatizantes == 0)
            {
                return Ok(new List<SimpatizantesEstadisticaDTO>());
            }

            var programasSociales = await context.ProgramasSociales
                .Include(p => p.Simpatizantes).ToListAsync();

            var estadisticas = programasSociales
                .Select(programa => new SimpatizantesEstadisticaDTO
                {
                    Id = programa.Id,
                    Nombre = programa.Nombre,
                    TotalSinpatizantes = programa.Simpatizantes?.Count ?? 0,
                    Porcentaje = programa.Simpatizantes?.Count * 100 / totalSimpatizantes ?? 0
                })
                .ToList();

            return Ok(estadisticas);
        }
        [HttpGet("total-Simpatizantes-por-edad")]
        public async Task<ActionResult<List<SimpatizanteEstatdisticasEdades>>> TotalSimpatizantesPorEdad()
        {
            var simpatizantes = await context.Simpatizantes.ToListAsync();
            var totalSimpatizantes = simpatizantes.Count;

            if (totalSimpatizantes == 0)
            {
                return Ok(new List<SimpatizanteEstatdisticasEdades>());
            }

            // Calcula la edad y clasifica por rangos
            var estadisticas = simpatizantes
                .Select(s => new
                {
                    Edad = CalcularEdad(s.FechaNacimiento),
                })
                .GroupBy(s => ObtenerRangoEdad(s.Edad))
                .Select(g => new SimpatizanteEstatdisticasEdades
                {
                    
                    RangoEdad = ObtenerRangoEdadTexto(g.Key),
                    TotalSinpatizantes = g.Count(),
                    Porcentaje = (decimal)g.Count() * 100 / totalSimpatizantes
                })
                .ToList();

            return Ok(estadisticas);
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - fechaNacimiento.Year;

            if (fechaNacimiento > fechaActual.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }

        private string ObtenerRangoEdad(int edad)
        {
            if (edad >= 18 && edad <= 28)
            {
                return "18-28";
            }
            else if (edad >= 29 && edad <= 39)
            {
                return "29-39";
            }
            else if (edad >= 40 && edad <= 50)
            {
                return "40-50";
            }
            else
            {
                return "51+";
            }
        }

        private string ObtenerRangoEdadTexto(string rangoEdad)
        {
            switch (rangoEdad)
            {
                case "18-28":
                    return "18 a 28 años";
                case "29-39":
                    return "29 a 39 años";
                case "40-50":
                    return "40 a 50 años";
                case "51+":
                    return "51 años o más";
                default:
                    return "Desconocido";
            }
        }

        //[HttpGet("total-Simpatizantes-por-genero")]
        //public async Task<ActionResult<List<SimpatizantesEstadisticaGeneroDTO>>> TotalSimpatizantesPorGenero()
        //{
        //    var simpatizantes = await context.Simpatizantes.ToListAsync();
        //    var totalSimpatizantes = simpatizantes.Count;

        //    if (totalSimpatizantes == 0)
        //    {
        //        return Ok(new List<SimpatizantesEstadisticaGeneroDTO>());
        //    }

        //    // Agrupa los simpatizantes por género
        //    var estadisticas = simpatizantes
        //        .GroupBy(s => s.Genero)
        //        .Select(g => new SimpatizantesEstadisticaGeneroDTO
        //        {
        //            Id = g.Key, 
        //            Nombre = Genero.Nombre, 
        //            TotalSinpatizantes = g.Count(),
        //            Porcentaje = (decimal)g.Count() * 100 / totalSimpatizantes
        //        })
        //        .ToList();

        //    return Ok(estadisticas);
        //}

        private string ObtenerNombreGenero(int idGenero)
        {
            return idGenero == 1 ? "Masculino" : "Femenino";
        }

        [HttpGet("total-general")]
        public async Task<ActionResult<TotalGeneralDTO>> TotalGeneral()
        {
            var totales = new TotalGeneralDTO()
            {
                TotalSimpatizantes = (await context.Simpatizantes.ToListAsync()).Count,
                TotalOperadores = (await context.Operadores.ToListAsync()).Count,
                TotalCandidatos = (await context.Candidatos.ToListAsync()).Count,
                TotalUsuarios = (await context.Usuarios.ToListAsync()).Count,
                TotalVisitas = (await context.Visitas.ToListAsync()).Count,
            };

            return Ok(totales);
        }
        [HttpGet("total-incidencias-jornada-electoral-por-Incidencia")]
        public async Task<ActionResult<List<IncidenciasJornadaEstadisticaDTO>>> TotalIncidenciasjornadaelectoralPortipoIncidencia()
        {
            var tipoincidencias = await context.TiposIncidencias.ToListAsync();
            
            var totalIncidencias = tipoincidencias.Count; 

            if (totalIncidencias == 0)
            {
                return Ok(new List<IncidenciasJornadaEstadisticaDTO>());
            }
            var Incidencias = await context.Incidencias
                .Include(p => p.TipoIncidencia).ToListAsync();

            var estadisticas = Incidencias
                .Select(incidencia => new IncidenciasJornadaEstadisticaDTO
                {
                    Id = incidencia.Id,
                    Retroalimentacion = incidencia.Retroalimentacion,
                     
        })
                .ToList();

            return Ok(estadisticas);
        }

    }
    
    

    }
