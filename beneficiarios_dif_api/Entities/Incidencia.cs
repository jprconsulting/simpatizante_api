namespace beneficiarios_dif_api.Entities
{
    public class Incidencia
    {
        public int Id { get; set; }
        public string Retroalimentacion { get; set; }
        public Indicador Indicador { get; set; }
        public Casilla Casilla { get; set; }

    }
}
