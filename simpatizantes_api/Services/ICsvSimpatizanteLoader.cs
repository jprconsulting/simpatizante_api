using System.Threading.Tasks;

namespace simpatizantes_api.Services
{
    public interface ICsvSimpatizanteLoader
    {
        Task<(int createdCount, int updatedCount)> LoadFromCsvAsync(string filePath);
    }
}

