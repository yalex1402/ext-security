using System.ComponentModel.DataAnnotations;

namespace ext_security.auth.BindingModels
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}