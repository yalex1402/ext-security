using System.ComponentModel.DataAnnotations;

namespace secure_lib.Models.BindingModels
{
    public class AssignRoleModel
    {
        [Required(ErrorMessage = "User's email address is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string RoleName { get; set; }
    }
}