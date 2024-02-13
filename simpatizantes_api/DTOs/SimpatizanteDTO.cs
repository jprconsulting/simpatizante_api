using System;

namespace simpatizantes_api.DTOs
{
    public class SimpatizanteDTO
    {
        public int? Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string StrFechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public GeneroDTO Genero { get; set; }
        public string CURP { get; set; }
        public string Numerotel { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Estatus { get; set; }
        public string ClaveElector { get; set; }
        public int Edad => CalcularEdad(FechaNacimiento);
        public ProgramaSocialDTO ProgramaSocial { get; set; }
        public SeccionDTO Seccion { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public EstadoDTO Estado { get; set; }
        public OperadorDTO Operador { get; set; }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
                edad--;
            return edad;
        }
    }
}
