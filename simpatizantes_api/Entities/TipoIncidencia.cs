namespace simpatizantes_api.Entities
{
    public class TipoIncidencia
    {        
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Color { get; set; }
        public List<Incidencia> Incidencias { get; set; }
    }
}
