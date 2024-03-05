using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class DistribucionCandidaturaDTO
    {
        public int? Id { get; set; }
        public TipoEleccionDTO TipoEleccion { get; set; }
        public DistritoDTO Distrito { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public ComunidadDTO Comunidad { get; set; }
        public CandidaturaDTO Candidatura { get; set; }
    } 
}
