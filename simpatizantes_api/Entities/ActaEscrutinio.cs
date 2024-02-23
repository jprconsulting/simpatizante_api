namespace simpatizantes_api.Entities
{
    public class ActaEscrutinio
    {
        public int Id { get; set; }
        public Distrito Distrito { get; set; }
        public Municipio Municipio { get; set; }
        public Seccion Seccion { get; set; }
        public Casilla Casilla { get; set; }
        public TipoEleccion TipoEleccion { get; set; }

    }
}
