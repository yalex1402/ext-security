using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using secure_lib.Data.Entities.Security;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Configuration;
using secure_lib.Services.Interfaces.Security;

namespace secure_lib.Services.Security
{
    public class JwtTokenFactoryService : IJwtTokenFactoryService
    {
        private readonly JwtConfig _tokenConfig;

        public JwtTokenFactoryService(IOptions<JwtConfig> jwtConfig)
        {
            _tokenConfig = jwtConfig.Value;
        }
        
        public TokenModel GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId,user.Id),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenCreated = tokenHandler.CreateToken(tokenDescriptor);
            var result = new {
                token = tokenHandler.WriteToken(tokenCreated),
                expiration = tokenCreated.ValidTo.ToLocalTime()
            };
            return new TokenModel(result.token,result.expiration);
        }
    }
}