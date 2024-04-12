using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class ResultadoPreEliminarDTO
    {
        public int? Id { get; set; }
        public TipoEleccionDTO TipoEleccion { get; set; }
        public EstadoDTO Estado { get; set; }
        public DistritoDTO Distrito { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public ComunidadDTO Comunidad { get; set; }
        public SeccionDTO Seccion { get; set; }
        public CasillaDTO Casilla { get; set; }
        public string BoletasSobrantes { get; set; }
        public string PersonasVotaron { get; set; }
        public string VotosRepresentantes { get; set; }
        public string Suma { get; set; }
        public string NoRegistrado { get; set; }
        public string VotosNulos { get; set; }
        public List<string> Partidos { get; set; }
    }
}
