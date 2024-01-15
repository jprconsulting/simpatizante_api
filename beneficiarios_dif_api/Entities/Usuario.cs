using System.ComponentModel.DataAnnotations;

namespace beneficiarios_dif_api.Entities
{
    public class Usuario
    {
        public int Id { get; set; }      
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
        [Required]
        public int RolId { get; set; }
        public Rol Rol { get; set; } = null!;

        public int? CandidatoId { get; set; }
        public Candidato Candidato { get; set; } = null!;
        public int? OperadorId { get; set; }
        public Operador Operador { get; set; } = null!;



    }
}
