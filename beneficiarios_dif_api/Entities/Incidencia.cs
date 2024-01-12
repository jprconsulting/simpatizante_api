namespace beneficiarios_dif_api.Entities
{
    public class Incidencia
    {
        public int Id { get; set; }
        public string Retroalimentacion { get; set; }
        public string Foto { get; set; }
        public string Direccion { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public TipoIncidencia TipoIncidencia { get; set; }
        public Casilla Casilla { get; set; }

    }
}
