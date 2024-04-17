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
                            existingSimpatizante.Latitud = decimal.Parse(data[7]);
                            existingSimpatizante.Longitud = decimal.Parse(data[8]);
                            existingSimpatizante.Estatus = bool.Parse(data[9]);
                            existingSimpatizante.ClaveElector = data[10];
                            existingSimpatizante.TercerNivelContacto = data[11];
                            existingSimpatizante.ProgramaSocialId = int.Parse(data[12]);
                            existingSimpatizante.PromotorId = int.Parse(data[13]);
                            existingSimpatizante.SeccionId = int.Parse(data[14]);
                            existingSimpatizante.MunicipioId = int.Parse(data[15]);
                            existingSimpatizante.EstadoId = int.Parse(data[16]);
                            existingSimpatizante.OperadorId = int.Parse(data[17]);
                            existingSimpatizante.GeneroId = int.Parse(data[18]);

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
                                Latitud = decimal.Parse(data[7]),
                                Longitud = decimal.Parse(data[8]),
                                Estatus = bool.Parse(data[9]),
                                ClaveElector = data[10],
                                TercerNivelContacto = data[11],
                                ProgramaSocialId = int.Parse(data[12]),
                                PromotorId = int.Parse(data[13]),
                                SeccionId = int.Parse(data[14]),
                                MunicipioId = int.Parse(data[15]),
                                EstadoId = int.Parse(data[16]),
                                OperadorId = int.Parse(data[17]),
                                GeneroId = int.Parse(data[18])
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

    }
}
