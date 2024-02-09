namespace simpatizantes_api.Entities
{
    public class OperadorSeccion
    {
        public int Id { get; set; }
        public int OperadorId { get; set; }
        public Operador Operador { get; set; }
        public int SeccionId { get; set; }         
        public Seccion Seccion { get; set; }
    }
}
