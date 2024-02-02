namespace simpatizantes_api.DTOs
{
    public class SimpatizantesEstadisticaGeneroDTO : GeneroDTO
    {
        public int TotalSinpatizantes { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
