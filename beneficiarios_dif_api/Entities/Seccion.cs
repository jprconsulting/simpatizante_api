namespace beneficiarios_dif_api.Entities
{
    public class Seccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Localidad Localidad { get; set; }
    }
}