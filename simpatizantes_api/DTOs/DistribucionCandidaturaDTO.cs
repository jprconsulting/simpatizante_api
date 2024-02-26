using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class DistribucionCandidaturaDTO
    {
        public int? Id { get; set; }
        public TipoEleccionDTO TipoEleccion { get; set; }
        public DistritoDTO Distrito { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public string Nombre { get; set; }
    }
}
