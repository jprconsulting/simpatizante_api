using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class DistribucionOrdenadaDTO
    {
        public int? Id { get; set; }
        public string InputId { get; set; }
        public bool Orden { get; set; }
        public DistribucionCandidaturaDTO DistribucionCandidatura { get; set; }
        public TipoAgrupacionPoliticaDTO TipoAgrupacionPolitica { get; set; }
        public CandidaturaDTO Candidatura { get; set; }
        public CombinacionDTO Combinacion { get; set; }
        public int PadreId { get; set; }
        public string NombreCandidatura { get; set; }
        public string Logo { get; set; }
        public string ImagenBase64 { get; set; }

    }
}
