﻿using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class ComunidadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public MunicipioDTO Municipio { get; set; }

    }
}
