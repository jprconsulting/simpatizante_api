namespace simpatizantes_api.Entities
{
    public class Simpatizante
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public string CURP { get; set; }
        public string Numerotel { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Estatus { get; set; }
        public string ClaveElector { get; set; }
        public string TercerNivelContacto { get; set; }
        public Genero Genero { get; set; }
        public string? UsuarioCreacionNombre { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string? UsuarioEdicionNombre { get; set; }
        public DateTime? FechaHoraEdicion { get; set; }

        // Propiedades para almacenar los IDs de las relaciones
        public int? GeneroId { get; set; }
        public int? ProgramaSocialId { get; set; }
        public int? PromotorId { get; set; }
        public int? SeccionId { get; set; }
        public int? MunicipioId { get; set; }
        public int? EstadoId { get; set; }

        // Relaciones de navegación
        public ProgramaSocial ProgramaSocial { get; set; }
        public Promotor Promotor { get; set; }
        public Seccion Seccion { get; set; }
        public Municipio Municipio { get; set; }
        public Estado Estado { get; set; }
        public int OperadorId { get; set; }
        public Operador Operador { get; set; }
        public List<Visita> Visitas { get; set; }
        public List<Voto> Votos { get; set; }
    }
}
