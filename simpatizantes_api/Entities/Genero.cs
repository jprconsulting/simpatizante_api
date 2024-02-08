namespace simpatizantes_api.Entities
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Simpatizante> Simpatizante { get; set; }
        public List<Candidato> Candidato { get; set; }

    }
}
