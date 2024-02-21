namespace simpatizantes_api.Entities
{
    public class Operador
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public string? UsuarioCreacionNombre { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string? UsuarioEdicionNombre { get; set; }
        public DateTime? FechaHoraEdicion { get; set; }
        public int? CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
        public List<Simpatizante> Simpatizantes { get; set; }
        public List<OperadorSeccion> OperadorSecciones { get; set; }
        public List<PromotorOperador> PromotorOperadores { get; set; }
        public Usuario? Usuario { get; set; }

    }
}
