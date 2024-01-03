namespace beneficiarios_dif_api.Entities
{
    public class AreaAdscripcion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public List<ProgramaSocial> ProgramasSociales { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
