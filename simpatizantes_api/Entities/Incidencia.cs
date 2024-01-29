namespace simpatizantes_api.Entities
{
    public class Incidencia
    {
        public int Id { get; set; }
        public string Retroalimentacion { get; set; }
        public string Foto { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public TipoIncidencia TipoIncidencia { get; set; }
        public Casilla Casilla { get; set; }

    }
}
