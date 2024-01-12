namespace beneficiarios_dif_api.Entities
{
    public class Operador
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public Seccion Seccion { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Visita> Visitas { get; set; }
    }
}
