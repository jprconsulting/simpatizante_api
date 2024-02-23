namespace simpatizantes_api.Entities
{
    public class Candidatura
    {
        public int Id { get; set; }
        public TipoAgrupacionPolitica TipoAgrupacionPolitica { get; set; }
        public string Nombre { get; set;}
        public string Logo { get; set; }
        public bool Estatus { get; set; }
        public string Partidos { get; set; }
        public int Orden { get; set; }
    }
}
