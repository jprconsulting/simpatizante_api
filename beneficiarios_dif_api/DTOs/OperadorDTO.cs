using beneficiarios_dif_api.Entities;

namespace beneficiarios_dif_api.DTOs
{
    public class OperadorDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string StrFechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public SeccionDTO Seccion { get; set; }
        public string NombreCompleto { get; set; }

    }
}
