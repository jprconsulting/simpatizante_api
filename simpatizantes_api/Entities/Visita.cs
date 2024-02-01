namespace simpatizantes_api.Entities
{
    public class Visita
    {
        public int Id { get; set; }
        public string Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public DateTime FechaHoraVisita { get; set; }
        public Simpatizante Simpatizante { get; set; }
        public Usuario Usuario { get; set; }

    }
}
