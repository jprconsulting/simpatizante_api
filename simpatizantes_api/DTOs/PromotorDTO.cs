namespace simpatizantes_api.DTOs
{
    public class PromotorDTO
    {
        public int? Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public List<int> OperadoresIds { get; set; }
        public List<OperadorDTO> Operadores { get; set; }

        public string NombreCompleto
        {
            get { return $"{Nombres} {ApellidoPaterno} {ApellidoMaterno}"; }
        }
    }
}
