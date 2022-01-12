using System.ComponentModel.DataAnnotations;

namespace ext_security.auth.BindingModels
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}