using beneficiarios_dif_api.Entities;

namespace beneficiarios_dif_api.DTOs
{
    public class SeccionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public LocalidadDTO Localidad { get; set; }
    }
}