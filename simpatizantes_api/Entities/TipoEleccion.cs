namespace simpatizantes_api.Entities
{
    public class TipoEleccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ActaEscrutinio> ActasEscrutinios { get; set; }
        public List<DistribucionCandidatura> DistribucionesCandidaturas { get; set; }
        public List<ResultadoPreEliminar> ResultadosPreEliminares { get; set; } 

    }
}
