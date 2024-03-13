namespace simpatizantes_api.Entities
{
    public class Casilla
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public List<Incidencia> Incidencias { get; set; }
        public List<ActaEscrutinio> ActasEscrutinios { get; set; }
        public List<ResultadoPreEliminar> ResultadosPreEliminares { get; set; }

    }
}
