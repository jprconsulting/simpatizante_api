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
        public DateTime? FechaNacimiento { get; set; }
        public string StrFechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public GeneroDTO Genero { get; set; }
        public string CURP { get; set; }
        public string Numerotel { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Estatus { get; set; }
        public string TercerNivelContacto { get; set; }
        public int Edad => CalcularEdad(FechaNacimiento);
        public PromotorDTO Promotor { get; set; }
        public ProgramaSocialDTO ProgramaSocial { get; set; }
        public SeccionDTO Seccion { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public EstadoDTO Estado { get; set; }
        public OperadorDTO Operador { get; set; }
        public UsuarioDTO UsuarioCreacion { get; set; }
        public UsuarioDTO UsuarioEdicion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public DateTime? FechaHoraEdicion { get; set; }
        private int CalcularEdad(DateTime? fechaNacimiento)
        {

            if (fechaNacimiento.HasValue) 
            {
                var edad = DateTime.Today.Year - fechaNacimiento?.Year;
                edad = edad.HasValue ? edad : 0;
                if (fechaNacimiento?.Date > DateTime.Today.AddYears(-(int)edad))
                    edad--;

                if (edad > 120)
                {
                    return 0;
                }

                return (int)edad;
            }else
            {
                return 0;
            }
           
        }
    }
}
