using System;
using secure_lib.Models.BindingModels;

namespace secure_lib.Models.Dto
{
    public class TokenResponseDtoModel
    {
        public TokenResponseDtoModel(int statusCode , string message, TokenModel token)
        {
            StatusCode = statusCode;
            Message = message;
            Token = token;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TokenModel Token { get; set; }
    }
}