using System;
using System.ComponentModel.DataAnnotations;

namespace secure_lib.Models.BindingModels
{
    public class TokenModel
    {
        public TokenModel(string token, DateTime expiresIn)
        {
            TokenGenerated = token;
            ExpiresIn = expiresIn;
        }
        public string TokenGenerated { get; }       
        public DateTime ExpiresIn { get; }
    }
}