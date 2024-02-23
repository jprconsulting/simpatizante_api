using System;

namespace simpatizantes_api.Entities
{
    public class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime LastAccessTime { get; set; }
    }
}
