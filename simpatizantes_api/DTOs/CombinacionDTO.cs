using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class CombinacionDTO
    {
        public int Id { get; set; }
        public CandidaturaDTO Candidatura { get; set; }
        public string Logo { get; set; }
        public string Nombre { get; set; }
        public List<string> Partidos { get; set; }
        public string ImagenBase64 { get; set; }
    }
}
