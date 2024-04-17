using Microsoft.EntityFrameworkCore;
using simpatizantes_api.Entities;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace simpatizantes_api.Services
{
    public class CsvSimpatizanteLoader : ICsvSimpatizanteLoader
    {
        private readonly ApplicationDbContext _context;

        public CsvSimpatizanteLoader(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(int createdCount, int updatedCount)> LoadFromCsvAsync(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    int createdCount = 0;
                    int updatedCount = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(',');

                        if (data.Length < 18)
                        {
                            Console.WriteLine($"Error: No hay suficientes campos en la línea: {line}");
                            continue;
                        }

                        var nombres = data[0];
                        var apellidoPaterno = data[1];
                        var apellidoMaterno = data[2];

                        if (!DateTime.TryParse(data[3], out DateTime fechaNacimiento))
                        {
                            Console.WriteLine($"Error: Fecha de nacimiento inválida en la línea: {line}");
                            continue;
                        }

                        // Obtener los IDs correspondientes a los nombres proporcionados
                        var generoId = await GetGeneroIdByNameAsync(data[17]);
                        var programaSocialId = await GetProgramaSocialIdByNameAsync(data[11]);
                        var promotorId = await GetPromotorIdByNameAsync(data[12]);
                        var seccionId = await GetSeccionIdByNameAsync(data[13]);
                        var municipioId = await GetMunicipioIdByNameAsync(data[14]);
                        var estadoId = await GetEstadoIdByNameAsync(data[15]);
                        var operadorId = !string.IsNullOrEmpty(data[16]) ? await GetOperadorIdByNameAsync(data[16]) : null;


                        var latitud = !string.IsNullOrEmpty(data[7]) ? decimal.Parse(data[7]) : 1.1m;
                        var longitud = !string.IsNullOrEmpty(data[8]) ? decimal.Parse(data[8]) : 1.1m;

                        var existingSimpatizante = await _context.Simpatizantes.FirstOrDefaultAsync(s =>
                            s.Nombres == nombres &&
                            s.ApellidoPaterno == apellidoPaterno &&
                            s.ApellidoMaterno == apellidoMaterno);

                        if (existingSimpatizante != null)
                        {
                            // Actualizar el simpatizante existente con los nuevos datos
                            existingSimpatizante.FechaNacimiento = DateTime.Parse(data[3]);
                            existingSimpatizante.Domicilio = data[4];
                            existingSimpatizante.CURP = data[5];
                            existingSimpatizante.Numerotel = data[6];
                            existingSimpatizante.Latitud = latitud;
                            existingSimpatizante.Longitud = longitud;
                            existingSimpatizante.Estatus = true;
                            existingSimpatizante.ClaveElector = data[9];
                            existingSimpatizante.TercerNivelContacto = data[10];
                            existingSimpatizante.ProgramaSocialId = programaSocialId;
                            existingSimpatizante.PromotorId = promotorId;
                            existingSimpatizante.SeccionId = seccionId;
                            existingSimpatizante.MunicipioId = municipioId;
                            existingSimpatizante.EstadoId = estadoId;
                            existingSimpatizante.OperadorId = operadorId;
                            existingSimpatizante.GeneroId = generoId;

                            updatedCount++;
                        }
                        else
                        {
                            // Crear un nuevo simpatizante
                            var newSimpatizante = new Simpatizante
                            {
                                Nombres = nombres,
                                ApellidoPaterno = apellidoPaterno,
                                ApellidoMaterno = apellidoMaterno,
                                FechaNacimiento = DateTime.Parse(data[3]),
                                Domicilio = data[4],
                                CURP = data[5],
                                Numerotel = data[6],
                                Latitud = latitud,
                                Longitud = longitud,
                                Estatus = true,
                                ClaveElector = data[9],
                                TercerNivelContacto = data[10],
                                ProgramaSocialId = programaSocialId,
                                PromotorId = promotorId,
                                SeccionId = seccionId,
                                MunicipioId = municipioId,
                                EstadoId = estadoId,
                                OperadorId = operadorId,
                                GeneroId = generoId
                            };

                            _context.Simpatizantes.Add(newSimpatizante);
                            createdCount++;
                        }
                    }

                    await _context.SaveChangesAsync();
                    return (createdCount, updatedCount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo CSV: {ex.Message}");
                return (0, 0);
            }
        }

        private async Task<int?> GetGeneroIdByNameAsync(string name)
        {
            var genero = await _context.Generos.FirstOrDefaultAsync(e => e.Nombre == name);
            return genero?.Id;
        }

        private async Task<int?> GetProgramaSocialIdByNameAsync(string name)
        {
            var programaSocial = await _context.ProgramasSociales.FirstOrDefaultAsync(e => e.Nombre == name);
            return programaSocial?.Id;
        }

        private async Task<int?> GetPromotorIdByNameAsync(string name)
        {
            var promotor = await _context.Promotores.FirstOrDefaultAsync(e => e.Nombres == name);
            return promotor?.Id;
        }

        private async Task<int?> GetSeccionIdByNameAsync(string name)
        {
            var seccion = await _context.Secciones.FirstOrDefaultAsync(e => e.Nombre == name);
            return seccion?.Id;
        }

        private async Task<int?> GetMunicipioIdByNameAsync(string name)
        {
            var municipio = await _context.Municipios.FirstOrDefaultAsync(e => e.Nombre == name);
            return municipio?.Id;
        }

        private async Task<int?> GetEstadoIdByNameAsync(string name)
        {
            var estado = await _context.Estados.FirstOrDefaultAsync(e => e.Nombre == name);
            return estado?.Id;
        }

        private async Task<int?> GetOperadorIdByNameAsync(string name)
        {
            var operador = await _context.Operadores.FirstOrDefaultAsync(e => e.Nombres == name);
            return operador?.Id;
        }
    }
}
