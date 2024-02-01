namespace simpatizantes_api.DTOs
{
    public class IncidenciasJornadaEstadisticaDTO : IncidenciaDTO 
    {
        public int TotalIncidencias { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
