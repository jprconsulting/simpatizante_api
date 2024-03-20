namespace simpatizantes_api.Entities
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sobrenombre { get; set; }
        public string Foto { get; set; }
        public string Emblema { get; set; }
        public bool Estatus { get; set; }
        public string? UsuarioCreacionNombre { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string? UsuarioEdicionNombre { get; set; }
        public DateTime? FechaHoraEdicion { get; set; }
        public Genero Genero { get; set; }
        public Cargo Cargo { get; set; }
        public int? EstadoId { get; set; }
        public Estado? Estado { get; set; } 
        public int? DistritoId { get; set; }
        public Distrito? Distrito { get; set; }
        public int? MunicipioId { get; set; }
        public Municipio? Municipio { get; set; }
        public int? ComunidadId { get; set; }
        public Comunidad? Comunidad { get; set; }
        public List <Operador> Operador { get; set; }
        public Usuario? Usuario { get; set; }


    }
}