using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace secure_lib.Data.Entities.Security
{
    public class Role : GeneralEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public List<User> UsersAssigned { get; set; }
    }
}