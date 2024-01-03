namespace beneficiarios_dif_api.DTOs
{
    public class ProgramaSocialEstadisticaDTO : ProgramaSocialDTO
    {
        public int TotalBeneficiarios { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
