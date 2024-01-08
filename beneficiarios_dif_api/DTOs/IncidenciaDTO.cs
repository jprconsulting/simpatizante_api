using beneficiarios_dif_api.Entities;

namespace beneficiarios_dif_api.DTOs
{
    public class IncidenciaDTO
    {
        public int Id { get; set; }
        public string Retroalimentacion { get; set; }
        public Indicador Indicador { get; set; }
        public Casilla Casilla { get; set; }
    }
}
