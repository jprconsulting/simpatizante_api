namespace simpatizantes_api.Entities
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
        public List<Estado> Estados { get; set; }

    }
}
