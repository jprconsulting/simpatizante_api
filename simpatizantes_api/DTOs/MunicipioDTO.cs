﻿namespace simpatizantes_api.DTOs
{
    public class MunicipioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }    
        public EstadoDTO Estado { get; set; }
    }
}