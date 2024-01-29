namespace simpatizantes_api.DTOs
{
    public class OperadorDTO
    {
        public int? Id { get; set; }
        public string Nombres { get; set; }
        public string NombreCompleto { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string StrFechaNacimiento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public List<int> SeccionesIds { get; set; }
        public List<SeccionDTO> Secciones { get; set; }
    }
}
