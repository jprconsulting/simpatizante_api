namespace simpatizantes_api.Entities
{
    public class ResultadoPreEliminar
    {
        public int Id { get; set; }
        public TipoEleccion TipoEleccion { get; set; }
        public int? DistritoId { get; set; }
        public Distrito? Distrito { get; set; }
        public int? MunicipioId { get; set; }
        public Municipio? Municipio { get; set; }
        public int? ComunidadId { get; set; }
        public Comunidad? Comunidad { get; set; }
        public Seccion Seccion { get; set; }
        public Casilla Casilla { get; set; }
        public string BoletasSobrantes { get; set; }
        public string PersonasVotaron { get; set; }
        public string VotosRepresentantes { get; set; }
        public string Suma { get; set; }
        public string Partidos { get; set; }
    }
}
