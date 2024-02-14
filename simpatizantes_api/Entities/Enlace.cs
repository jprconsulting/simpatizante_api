namespace simpatizantes_api.Entities
{
    public class Enlace
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public List<Simpatizante> Simpatizantes { get; set; }
    }
}
