namespace simpatizantes_api.Entities
{
    public class Promotor
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string? UsuarioCreacionNombre { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string? UsuarioEdicionNombre { get; set; }
        public DateTime? FechaHoraEdicion { get; set; }
        public List<Simpatizante> Simpatizantes { get; set; }
        public List<PromotorOperador> PromotorOperadores { get; set; }
    }
}
