namespace simpatizantes_api.Entities
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; set; }
        public List<Seccion> Secciones { get; set; }
        public List<Simpatizante> Simpatizantes { get; set; }
        public List<ActaEscrutinio> ActasEscrutinios { get; set; }
        public List<DistribucionCandidatura> DistribucionesCandidaturas { get; set; }
        public List <Municipio> Municipios { get; set; }
    }
}
