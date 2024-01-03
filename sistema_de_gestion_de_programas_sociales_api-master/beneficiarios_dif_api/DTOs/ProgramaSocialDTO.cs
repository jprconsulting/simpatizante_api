namespace beneficiarios_dif_api.DTOs
{
    public class ProgramaSocialDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public bool Estatus { get; set; }
        public string Acronimo { get; set; }       
        public AreaAdscripcionDTO AreaAdscripcion { get; set; }
    }
}
