using System.Threading.Tasks;

namespace simpatizantes_api.Services
{
    public interface ICsvSimpatizanteLoader
    {
        Task<int> LoadFromCsvAsync(string filePath);
    }
}
