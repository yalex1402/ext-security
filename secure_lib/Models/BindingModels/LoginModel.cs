using System.ComponentModel.DataAnnotations;

namespace secure_lib.Models.BindingModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}