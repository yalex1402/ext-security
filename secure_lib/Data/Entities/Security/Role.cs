using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace secure_lib.Data.Entities.Security
{
    public class Role : GeneralEntity
    {
        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public List<User> UsersAssigned { get; set; }
    }
}