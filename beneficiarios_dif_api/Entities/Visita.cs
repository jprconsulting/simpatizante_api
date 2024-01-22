using beneficiarios_dif_api.DTOs;

namespace beneficiarios_dif_api.Entities
{
    public class Visita
    {
        public int Id { get; set; }
        public string Servicio { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public string ImagenBase64 { get; set; }
        public DateTime FechaHoraVisita { get; set; }
        public Votante Votante { get; set; }
        public int? CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
        public int? OperadorId { get; set; }
        public Operador Operador { get; set; }
    }
}
