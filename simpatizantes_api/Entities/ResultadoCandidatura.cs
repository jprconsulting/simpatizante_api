namespace simpatizantes_api.Entities
{
    public class ResultadoCandidatura
    {
        public int Id { get; set; }
        public ActaEscrutinio ActaEscrutinio { get; set; }  
        public DistribucionCandidatura DistribucionCandidatura { get; set; }
        public Candidatura Candidatura { get; set; }
        public Combinacion Combinacion { get; set; }
        public int PadreId { get; set; }
        public int VotoPreliminar { get; set; }

    }
}
