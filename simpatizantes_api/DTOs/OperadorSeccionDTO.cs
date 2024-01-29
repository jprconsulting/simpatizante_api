namespace simpatizantes_api.DTOs
{
    public class OperadorSeccionDTO
    {
        public int Id { get; set; }
        public OperadorDTO Operador { get; set; }
        public SeccionDTO Seccion { get; set; }
    }
}
