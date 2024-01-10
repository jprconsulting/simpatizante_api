namespace beneficiarios_dif_api.Entities
{
    public class Localidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Municipio Municipio { get; set; }
    }
}