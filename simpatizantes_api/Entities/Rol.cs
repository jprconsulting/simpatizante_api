namespace simpatizantes_api.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public string NombreRol { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
