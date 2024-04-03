namespace simpatizantes_api.Entities
{
    public class PropagandaElectoral
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public string Comentarios { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Ubicacion { get; set; }
        public string Dimensiones { get; set; }
        public string Foto { get; set; }
        public Municipio Municipio { get; set; }
        public Seccion Seccion { get; set; } 
        public Candidato Candidato { get; set; }
    }
}
