
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using simpatizantes_api.Filters;
using static System.Collections.Specialized.BitVector32;


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
            var comments = await context.visitas.Select(v => v.Servicio).ToListAsync();
            var wordCount = CountWords(comments);

            var generalWordCloud = new GeneralWordCloudDTO
            {
                WordCloudPorCandidatos = new List<CandidatoWordCloudDTO>(),
                WordCloudPorMunicipios = new List<MunicipioWordCloudDTO>(),

                GeneralWordCloud = CreateModel(wordCount)
            };

            var secciones = await context.candidatos.ToListAsync(); 

            foreach (var seccion in secciones)
            {
                var visitasPorseccion = await context.visitas
                    .Include(v => v.Simpatizante)
                    .ThenInclude(b => b.Operador) 
                    .Where(v => v.Simpatizante.Operador.CandidatoId == seccion.Id) 
                    .ToListAsync();

                var commentsByseccion = visitasPorseccion.Select(v => v.Servicio).ToList();
                var wordCountByseccion = CountWords(commentsByseccion);
                var seccionWordCloud = new CandidatoWordCloudDTO
                {
                    Id = seccion.Id,
                    Nombres = seccion.Nombres,
                    ApellidoMaterno = seccion.ApellidoMaterno,
                    ApellidoPaterno = seccion.ApellidoPaterno,
                    WordCloud = CreateModel(wordCountByseccion)
                };
                generalWordCloud.WordCloudPorCandidatos.Add(seccionWordCloud);
            }
            var municipios = await context.secciones.ToListAsync();

            foreach (var municipio in municipios)
            {
                var visitasPorseccion = await context.visitas
                    .Include(v => v.Simpatizante)
                    .ThenInclude(b => b.Municipio)
                    .Where(v => v.Simpatizante.Seccion.Id == municipio.Id)
                    .ToListAsync();

                var commentsBymunicipio = visitasPorseccion.Select(v => v.Servicio).ToList();
                var wordCountBymunicipio = CountWords(commentsBymunicipio);
                var seccionWordCloud2 = new MunicipioWordCloudDTO
                {
                    Id = municipio.Id,
                    Clave = municipio.Clave,
                    Nombre = municipio.Nombre,
                    WordCloud = CreateModel(wordCountBymunicipio)
                };
                generalWordCloud.WordCloudPorMunicipios.Add(seccionWordCloud2);
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
            var Simpatizantes = await context.simpatizantes.ToListAsync();
            var totalSimpatizantes = Simpatizantes.Count;

            if (totalSimpatizantes == 0)
            {
                return Ok(new List<SimpatizantesEstadisticaDTO>());
            }

            var programasSociales = await context.programassociales
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
            var simpatizantes = await context.simpatizantes.ToListAsync();
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

[HttpGet("total-Simpatizantes-por-genero")]
public async Task<ActionResult<List<SimpatizantesEstadisticaGeneroDTO>>> TotalSimpatizantesPorGenero()
{
    var simpatizantes = await context.simpatizantes.Include(s => s.Genero).ToListAsync();
    var totalSimpatizantes = simpatizantes.Count;

    if (totalSimpatizantes == 0)
    {
        return Ok(new List<SimpatizantesEstadisticaGeneroDTO>());
    }

    var generos = await context.generos.ToListAsync();

    var estadisticas = generos
        .Select(genero => new SimpatizantesEstadisticaGeneroDTO
        {
            Id = genero.Id, // Mantener el ID si es necesario
            Nombre = ObtenerNombreGenero(genero.Id),
            TotalSinpatizantes = simpatizantes.Count(s => s.Genero.Id == genero.Id),
            Porcentaje = (decimal)simpatizantes.Count(s => s.Genero.Id == genero.Id) * 100 / totalSimpatizantes
        })
        .ToList();

    return Ok(estadisticas);
}

private string ObtenerNombreGenero(int idGenero)
{
    var genderNames = new Dictionary<int, string>
    {
        { 4, "Masculino" },
        { 5, "Femenino" },
        { 6, "No binario" },
    };

    return genderNames.TryGetValue(idGenero, out var name) ? name : "Desconocido";
}


        [HttpGet("total-general")]
        public async Task<ActionResult<TotalGeneralDTO>> TotalGeneral()
        {
            var totales = new TotalGeneralDTO()
            {
                TotalSimpatizantes = (await context.simpatizantes.ToListAsync()).Count,
                TotalOperadores = (await context.operadores.ToListAsync()).Count,
                TotalCandidatos = (await context.candidatos.ToListAsync()).Count,
                TotalUsuarios = (await context.usuarios.ToListAsync()).Count,
                TotalVisitas = (await context.visitas.ToListAsync()).Count,
            };

            return Ok(totales);
        }
        [HttpGet("total-incidencias-jornada-electoral-por-Incidencia")]
        public async Task<ActionResult<List<IncidenciasJornadaEstadisticaDTO>>> TotalIncidenciasjornadaelectoralPortipoIncidencia()
        {
            var tipoincidencias = await context.tiposincidencias.ToListAsync();
            
            var totalIncidencias = tipoincidencias.Count; 

            if (totalIncidencias == 0)
            {
                return Ok(new List<IncidenciasJornadaEstadisticaDTO>());
            }
            var Incidencias = await context.incidencias
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
