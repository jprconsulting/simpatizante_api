namespace simpatizantes_api.Entities
{
    public class Candidatura
    {
        public int Id { get; set; }
        public TipoAgrupacionPolitica TipoAgrupacionPolitica { get; set; }
        public string Nombre { get; set;}
        public string Logo { get; set; }
        public string Acronimo { get; set; }
        public bool Estatus { get; set; }
        public string Partidos { get; set; }
        public int Orden { get; set; }
        public List<Combinacion> Combinaciones { get; set; }
        public List<DistribucionOrdenada> DistribucionesOrdenadas { get; set; }
        public List<ResultadoCandidatura> ResultadosCandidaturas { get; set; }

    }
}
