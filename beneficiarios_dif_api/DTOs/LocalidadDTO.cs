using beneficiarios_dif_api.Entities;

namespace beneficiarios_dif_api.DTOs
{
    public class LocalidadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public MunicipioDTO Municipio { get; set; }
    }
}
