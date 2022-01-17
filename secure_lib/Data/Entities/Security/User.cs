using System.Collections.Generic;

namespace secure_lib.Data.Entities.Security
{
    public class User : GeneralEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool Status { get; set; }
        public Role UserRole { get; set; }
        public List<Session> SessionList { get; set; }
    }
}