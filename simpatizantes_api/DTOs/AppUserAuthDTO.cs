namespace simpatizantes_api.DTOs
{
    public class AppUserAuthDTO
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public List<ClaimDTO> Claims { get; set; }
    }
}
