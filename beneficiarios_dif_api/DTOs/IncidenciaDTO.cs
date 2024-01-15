using beneficiarios_dif_api.Entities;

namespace beneficiarios_dif_api.DTOs
{
    public class IncidenciaDTO
    {
        public int Id { get; set; }
        public string Retroalimentacion { get; set; }
        public string Foto { get; set; }
        public string ImagenBase64 { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public IndicadorDTO TipoIncidencia { get; set; }
        public CasillaDTO Casilla { get; set; }
    }
}
