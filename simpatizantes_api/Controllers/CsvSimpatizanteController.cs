using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simpatizantes_api.Services;
using System;
using System.Threading.Tasks;

namespace simpatizantes_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvSimpatizanteController : ControllerBase
    {
        private readonly ICsvSimpatizanteLoader _csvSimpatizanteLoader;

        public CsvSimpatizanteController(ICsvSimpatizanteLoader csvSimpatizanteLoader)
        {
            _csvSimpatizanteLoader = csvSimpatizanteLoader;
        }

        [HttpPost("cargar-csv")]
        public async Task<ActionResult> CargarCsv(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Archivo no proporcionado o vacío");
                }

                var filePath = $"uploads/{Guid.NewGuid()}_{file.FileName}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                var (createdCount, updatedCount) = await _csvSimpatizanteLoader.LoadFromCsvAsync(filePath);

                return Ok($"Se crearon {createdCount} simpatizantes y se actualizaron {updatedCount} simpatizantes desde el archivo CSV.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar el archivo CSV: {ex.Message}");
            }
        }

    }
}
