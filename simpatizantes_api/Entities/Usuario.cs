using System.ComponentModel.DataAnnotations;

namespace simpatizantes_api.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
        [Required]
        public Rol Rol { get; set; }
        public int? CandidatoId { get; set; }
        public Candidato? Candidato { get; set; }
        public int? OperadorId { get; set; }
        public Operador? Operador { get; set; }
        public List<Visita> Visitas { get; set; }
        public List<Simpatizante> Simpatizantes { get; set; }
    }
}
