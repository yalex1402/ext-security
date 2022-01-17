using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IRoleRepository _roleRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IJwtTokenFactoryService _tokenFactory;
        private readonly IPasswordService _passwordService;

        public UserManagerController(ILogger<UserManagerController> logger,
                                    IUserRepository repository,
                                    IRoleRepository roleRepository,
                                    IConverterHelper converterHelper,
                                    IJwtTokenFactoryService tokenFactory,
                                    IPasswordService passwordService)
        {
            _logger = logger;
            _repository = repository;
            _roleRepository = roleRepository;
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

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] AddUserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("User fields are not valid or fields are missing");
                }
                model.Id = Guid.NewGuid().ToString();
                model.DateCreated = DateTime.UtcNow;
                model.Status = true;
                model.Password = _passwordService.GenerateHashCode(model.Password);
                User entityModel = _converterHelper.ConvertTo<User, AddUserModel>(model);
                var result = await _repository.CreateUserAsync(entityModel);
                if (!result)
                {
                    return BadRequest("User cannot be created");
                }
                return Ok("User has been created successfully");
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

        [HttpPost("AssignRole")]
        [Authorize]
        public async Task<IActionResult> AssignRoleAsync([FromBody] AssignRoleModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Fields are not valid or missing");
                }
                User userFound = await _repository.GetUserByEmailAsync(model.Email);
                if (userFound == null)
                {
                    return NotFound("User does not exist");
                }
                Role roleFound = await _roleRepository.GetRoleByNameAsync(model.RoleName);
                if (roleFound == null)
                {
                    return NotFound("Role does not exist");
                }
                var result = await _repository.AssignRoleAsync(userFound, roleFound);
                if (!result)
                {
                    return BadRequest("Role has not been assigned");
                }
                return Ok($"Role {roleFound.Name} has already been assigned to {userFound.Name}");    
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