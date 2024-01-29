namespace simpatizantes_api.Entities
{
    public class Voto
    {
        public int Id { get; set; }
        public string Foto { get; set; }
        public DateTime FechaHoraVot { get; set; }
        public Simpatizante Simpatizante { get; set; } 
    }
}
