namespace beneficiarios_dif_api.Entities
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List <Municipio> Municipios { get; set; }
        public List<Simpatizante> Simpatizantes { get; set; }
    }
}
