using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ext_security.auth.BindingModels;
using ext_security.auth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ext_security.auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(ILogger<UserController> logger,
                            UserManager<AppUser> userManager,
                            SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<object> LoginAsync([FromBody] LoginModel model){
            try
            {
                if (!ModelState.IsValid)
                {
                    return await Task.FromResult("Parameters are missing");
                }
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,false,false);
                if (result.Succeeded)
                {
                    return await Task.FromResult($"Welcome back {model.UserName}");
                }
                return await Task.FromResult("Invalid email or password");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }
    }
}