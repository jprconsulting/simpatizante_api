namespace simpatizantes_api.DTOs
{
    public class SimpatizanteDTO
    {
        public int? Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public int Sexo { get; set; }
        public string CURP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Estatus { get; set; }
        public string IDMEX { get; set; }
        public ProgramaSocialDTO ProgramaSocial { get; set; }
        public SeccionDTO Seccion { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public EstadoDTO Estado { get; set; }
        public string NombreCompleto { get; set; }

    }

}