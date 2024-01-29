namespace simpatizantes_api.DTOs
{
    public class ClaimDTO
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public bool ClaimValue { get; set; }
        public int RolId { get; set; }
    }
}
