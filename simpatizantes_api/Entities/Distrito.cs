namespace simpatizantes_api.Entities
{
    public class Distrito
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Estado Estado { get; set; }
        public List<ActaEscrutinio> ActasEscrutinios { get; set; }
        public List <Municipio> Municipios { get; set; }
        public List<ResultadoPreEliminar> ResultadosPreEliminares { get; set; }

    }
}
