namespace beneficiarios_dif_api.Entities
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Beneficiario> Beneficiarios { get; set; }
    }
}
