namespace simpatizantes_api.DTOs
{
    public class SimpatizantesEstadisticaDTO : ProgramaSocialDTO
    {
        public int TotalSinpatizantes { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
