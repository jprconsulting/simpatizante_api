namespace simpatizantes_api.Entities
{
    public class Distrito
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ActaEscrutinio> ActasEscrutinios { get; set; }

    }
}
