namespace simpatizantes_api.Entities
{
    public class PromotorOperador
    {
        public int Id { get; set; }
        public int PromotorId { get; set; }
        public Promotor Promotor { get; set; }
        public int OperadorId { get; set; }
        public Operador Operador { get; set; }
   
    }
}
