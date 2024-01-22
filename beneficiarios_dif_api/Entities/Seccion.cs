namespace beneficiarios_dif_api.Entities
{
    public class Seccion
    {
        public int Id { get; set; }
        public string Clave { get; set; }        
        public Municipio Municipio { get; set; }
        public List<Votante> Votantes { get; set; }
    }
}