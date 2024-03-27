using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class DistribucionCandidaturaDTO
    {
        public int? Id { get; set; }
        public TipoEleccionDTO TipoEleccion { get; set; }
        public DistritoDTO Distrito { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public EstadoDTO Estado { get; set; }
        public ComunidadDTO Comunidad { get; set; }
        public List<string?> Partidos { get; set; }
        public List<string?> Coalicion { get; set; }
        public List<string?> Comun { get; set; }
        public List<string?> Independiente { get; set; }
        

    }
}
