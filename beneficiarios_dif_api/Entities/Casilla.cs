namespace beneficiarios_dif_api.Entities
{
    public class Casilla
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public List<Incidencia> Incidencias { get; set; }
    }
}
