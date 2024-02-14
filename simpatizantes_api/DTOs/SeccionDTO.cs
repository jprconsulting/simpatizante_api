namespace simpatizantes_api.DTOs
{
    public class SeccionDTO
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public MunicipioDTO Municipio { get; set; }

        public string ClaveYNombre 
        {
            get { return $"{Clave} - {Nombre}"; }
        }
    }
}