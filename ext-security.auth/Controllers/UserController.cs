using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ext_security.auth.BindingModels;
using ext_security.auth.Configuration;
using ext_security.auth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ext_security.auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtConfig _tokenConfig;

        public UserController(ILogger<UserController> logger,
                            UserManager<AppUser> userManager,
                            SignInManager<AppUser> signInManager,
                            IOptions<JwtConfig> jwtConfig)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenConfig = jwtConfig.Value;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<object> RegisterUserAsync([FromBody] AddUpdateUserModel model)
        {
            try
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    DateCreated = DateTime.UtcNow
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return await Task.FromResult("User has been registered successfully");
                }
                return await Task.FromResult(string.Join(",", result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<object> LoginAsync([FromBody] LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return await Task.FromResult("Parameters are missing");
                }
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    AppUser userLoggedIn = await _userManager.FindByEmailAsync(model.Email);
                    var user = new UserDTOModel(userLoggedIn.Email, userLoggedIn.UserName, userLoggedIn.FullName);
                    user.Token = GenerateToken(userLoggedIn);
                    return Ok(user);
                }
                return await Task.FromResult("Invalid email or password");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        private string GenerateToken(AppUser user)
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
            return tokenHandler.WriteToken(tokenCreated);
        }
    }
}