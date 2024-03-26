namespace simpatizantes_api.DTOs
{
    public class IncidenciaDTO
    {
        public int? Id { get; set; }
        public string Retroalimentacion { get; set; }
        public string Foto { get; set; }
        public string ImagenBase64 { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public TipoIncidenciaDTO TipoIncidencia { get; set; }
        public CasillaDTO Casilla { get; set; }
        public CandidatoDTO Candidato { get; set; }
    }
}
