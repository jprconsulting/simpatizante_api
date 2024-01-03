namespace beneficiarios_dif_api.DTOs
{
    public class IndicadorDTO
    {
        public int Id { get; set; }
        public int RangoInicial { get; set; }
        public int RangoFinal { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
    }
}
