using System;
using System.Collections.Generic;

namespace secure_lib.Data.Entities.Security
{
    public class Token
    {
        public string Id { get; set; }
        public string TokenCode { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
    }
}