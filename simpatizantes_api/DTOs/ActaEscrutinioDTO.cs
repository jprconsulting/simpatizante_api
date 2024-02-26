using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class ActaEscrutinioDTO
    {
        public int? Id { get; set; }
        public DistritoDTO Distrito { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public SeccionDTO Seccion { get; set; }
        public CasillaDTO Casilla { get; set; }
        public TipoEleccionDTO TipoEleccion { get; set; }
    }
}
