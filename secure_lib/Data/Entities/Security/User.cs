using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using secure_lib.Data.Entities;

namespace secure_lib.Data.Entities.Security
{
    public class User : GeneralEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "It's not a valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }
        public bool Status { get; set; }
        public Role UserRole { get; set; }
        public List<Session> SessionList { get; set; }
    }
}