namespace simpatizantes_api.Entities
{
    public class ActiveToken
    {
        public int Id { get; set; }
        public string TokenId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationTime { get; set; }
    }

}
