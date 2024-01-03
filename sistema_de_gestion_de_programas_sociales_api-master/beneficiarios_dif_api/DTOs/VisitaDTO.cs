namespace beneficiarios_dif_api.DTOs
{
    public class VisitaDTO
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public string StrFechaHoraVisita { get; set; }
        public string ImagenBase64 { get; set; }
        public BeneficiarioDTO Beneficiario { get; set; }
    }
}
