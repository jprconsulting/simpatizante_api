using beneficiarios_dif_api.Entities;

namespace beneficiarios_dif_api.DTOs
{
    public class CandidatoDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string StrFechaNacimiento { get; set; }
        public int Sexo { get; set; }
        public string Sobrenombre { get; set; }
        public string Foto { get; set; }
        public string Emblema { get; set; }
        public bool Estatus { get; set; }
        public CargoDTO Cargo { get; set; }
        public string ImagenBase64 { get; set; } 
        public string EmblemaBase64 { get; set; }
        public string NombreCompleto { get; set; }

    }
}
