namespace simpatizantes_api.Entities
{
    public class DistribucionOrdenada
    {
        public int Id { get; set; }
        public string InputId { get; set; }
        public bool Orden { get; set; }
        public DistribucionCandidatura DistribucionCandidatura { get; set; }
        public TipoAgrupacionPolitica TipoAgrupacionPolitica { get; set; }
        public Candidatura Candidatura { get; set; }
        public Combinacion Combinacion { get; set; }
        public int PadreId { get; set; }
        public string NombreCandidatura { get; set; }
        public string Logo { get; set; }
    }
}
