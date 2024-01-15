namespace beneficiarios_dif_api.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public string NombreRol { get; set; }
        public Usuario? Usuario { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
