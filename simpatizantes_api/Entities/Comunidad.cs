namespace simpatizantes_api.Entities
{
    public class Comunidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Municipio Municipio { get; set; }
    }
}
