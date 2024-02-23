﻿namespace simpatizantes_api.DTOs
{
    public class VisitaDTO
    {
        public int? Id { get; set; }
        public string Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public bool Simpatiza { get; set; }
        public string StrFechaHoraVisita { get; set; }
        public string ImagenBase64 { get; set; }
        public SimpatizanteDTO Simpatizante { get; set; }
        public UsuarioDTO Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public string RolUsuario { get; set; }
    }
}