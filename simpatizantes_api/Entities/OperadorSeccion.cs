namespace simpatizantes_api.Entities
{
    public class OperadorSeccion
    {
        public int Id { get; set; }
        public Operador Operador { get; set; }
        public Seccion Seccion { get; set; }
    }
}
