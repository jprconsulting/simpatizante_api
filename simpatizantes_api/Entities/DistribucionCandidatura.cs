namespace simpatizantes_api.Entities
{
    public class DistribucionCandidatura
    {
        public int Id { get; set; }
        public TipoEleccion TipoEleccion { get; set; }
        public Distrito Distrito { get; set; }
        public Municipio Municipio { get; set; }
        public string Nombre { get; set; }
    }
}
