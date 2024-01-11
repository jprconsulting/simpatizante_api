namespace beneficiarios_dif_api.Entities
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; set; }
        public List<Seccion> Secciones { get; set; }
        public List<Votante> Votantes { get; set; }

    }
}
