namespace simpatizantes_api.Entities
{
    public class Combinacion
    {
        public int Id { get; set; }
        public Candidatura Candidatura { get; set; }
        public string Logo { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public List<DistribucionOrdenada> DistribucionesOrdenadas { get; set; }
        public List<ResultadoCandidatura> ResultadosCandidaturas { get; set; }

    }
}
