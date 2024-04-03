using simpatizantes_api.Entities;

namespace simpatizantes_api.DTOs
{
    public class PropagandaElectoralDTO
    {
        public int? Id { get; set; }
        public string Folio { get; set; }
        public string Comentarios { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Ubicacion { get; set; } 
        public string Foto { get; set; }
        public string ImagenBase64 { get; set; }
        public string Dimensiones { get; set; }
        public MunicipioDTO Municipio { get; set; }
        public SeccionDTO Seccion { get; set; }
        public CandidatoDTO Candidato { get; set; }

    }
}
