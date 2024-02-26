namespace simpatizantes_api.Entities
{
    public class TipoAgrupacionPolitica
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Candidatura> Candidaturas { get; set; }
        public List<DistribucionOrdenada> DistribucionesOrdenadas { get; set; }

    }
}
