namespace simpatizantes_api.Entities
{
    public class Seccion
    {
        public int Id { get; set; }
        public string Clave { get; set; } 
        public string Nombre { get; set; }
        public Municipio Municipio { get; set; }
        public List<Simpatizante> Simpatizantes { get; set; }
        public List<OperadorSeccion> OperadorSecciones { get; set; }
        public List<ActaEscrutinio> ActasEscrutinios { get; set; }
        public List<ResultadoPreEliminar> ResultadosPreEliminares { get; set; }

    }
}