using System;
using secure_lib.Data.Entities.Security;

namespace secure_lib.Models.BindingModels
{
    public class AddSessionModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public Token Token { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }
        public User SessionUser { get; set; }
    }
}