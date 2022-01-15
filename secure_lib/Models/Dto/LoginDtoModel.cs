using System;

namespace secure_lib.Models.Dto
{
    public class LoginDtoModel
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        
    }
}