using System;
using System.ComponentModel.DataAnnotations;

namespace secure_lib.Models.BindingModels
{
    public class AddUpdateRoleModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}