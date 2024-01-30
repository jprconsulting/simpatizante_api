namespace simpatizantes_api.DTOs
{
    public class VotoDTO
    {
        public int? Id { get; set; }
        public string Foto { get; set; }
        public string ImagenBase64 { get; set; }
        public DateTime FechaHoraVot { get; set; }
        public bool EstatusVoto { get; set; }
        public SimpatizanteDTO Simpatizante { get; set; }
    }
}
