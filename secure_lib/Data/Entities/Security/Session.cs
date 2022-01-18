using System;

namespace secure_lib.Data.Entities.Security
{
    public class Session
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public Token Token { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }
        public User SessionUser { get; set; }
    }
}