﻿using System.ComponentModel.DataAnnotations;

namespace beneficiarios_dif_api.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
        [Required]
        public Rol Rol { get; set; }
        public int? CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
        public int? OperadorId { get; set; }
        public Operador Operador { get; set; }

    }
}
