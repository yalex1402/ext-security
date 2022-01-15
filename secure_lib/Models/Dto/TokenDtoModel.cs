using System;

namespace secure_lib.Models.Dto
{
    public class TokenDtoModel
    {
        public TokenDtoModel(string token, DateTime expiresIn)
        {
            TokenGenerated = token;
            ExpiresIn = expiresIn;
        }
        public string TokenGenerated { get; }       
        public DateTime ExpiresIn { get; }
    }
}