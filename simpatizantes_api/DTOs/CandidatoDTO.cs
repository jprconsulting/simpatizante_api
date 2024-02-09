﻿namespace simpatizantes_api.DTOs
{
    public class CandidatoDTO
    {
        public int? Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string StrFechaNacimiento { get; set; }
        public int Edad => CalcularEdad(FechaNacimiento);
        public GeneroDTO Genero { get; set; }
        public string Sobrenombre { get; set; }
        public string Foto { get; set; }
        public string Emblema { get; set; }
        public bool Estatus { get; set; }
        public CargoDTO Cargo { get; set; }
        public string ImagenBase64 { get; set; } 
        public string EmblemaBase64 { get; set; }
        public List<SimpatizanteDTO> Simpatizantes { get; set; }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;
            return edad;
        }
    }
}
