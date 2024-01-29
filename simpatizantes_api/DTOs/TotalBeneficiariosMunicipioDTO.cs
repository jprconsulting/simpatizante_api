namespace simpatizantes_api.DTOs
{
    public class TotalBeneficiariosMunicipioDTO : MunicipioDTO
    {
        public int TotalBeneficiarios { get; set; }
        public string Color { get; set; }
        public string DescripcionIndicador { get; set; }
    }
}
