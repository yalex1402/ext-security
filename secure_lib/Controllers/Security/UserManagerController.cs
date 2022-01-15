using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using secure_lib.Data.Entities.Security;
using secure_lib.Data.Interfaces.Repositories;
using secure_lib.Helpers.Interfaces;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Dto;
using secure_lib.Services.Interfaces.Security;
using secure_lib.Services.Interfaces.Utilities;

namespace secure_lib.Controllers.Security
{
    [ApiController]
    [Route ("api/[controller]")]
    public class UserManagerController : ControllerBase
    {
        private readonly ILogger<UserManagerController> _logger;
        private readonly IUserRepository _repository;
        private readonly IConverterHelper _converterHelper;
        private readonly IJwtTokenFactoryService _tokenFactory;
        private readonly IPasswordService _passwordService;

        public UserManagerController(ILogger<UserManagerController> logger,
                                    IUserRepository repository,
                                    IConverterHelper converterHelper,
                                    IJwtTokenFactoryService tokenFactory,
                                    IPasswordService passwordService)
        {
            _logger = logger;
            _repository = repository;
            _converterHelper = converterHelper;
            _tokenFactory = tokenFactory;
            _passwordService = passwordService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("User information is not valid");
                }
                User userFound = await _repository.GetUserByUserNameAsync(model.UserName);
                if (userFound == null)
                {
                    return NotFound($"User {model.UserName} is not registered");
                }
                //Compare Password
                bool isValidPassword = _passwordService.ValidateHashCode(model.Password,userFound.PasswordHash);
                if (!isValidPassword)
                {
                    return BadRequest("User or password is not valid");
                }
                //Generate TokenModel
                TokenModel tokenModel = _tokenFactory.GenerateToken(userFound);
                //Create Session
                
                return Ok(new TokenDtoModel(tokenModel.TokenGenerated,tokenModel.ExpiresIn));
            }
            catch (SqlException sqlex)
            {
                string innerMessage = sqlex.InnerException != null ? sqlex.InnerException.Message : string.Empty;
                string errorMessage = $"{sqlex.Message} - InnerException: {innerMessage}";
                return this.Problem(sqlex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                string innerMessage = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                string errorMessage = $"{ex.Message} - InnerException: {innerMessage}";
                return this.Problem(errorMessage, statusCode: 400);
            }
        }
    }
}