using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using secure_lib.Data.Entities.Security;
using secure_lib.Data.Interfaces.Repositories;
using secure_lib.Helpers.Interfaces;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Dto;

namespace secure_lib.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleManagerController : ControllerBase
    {
        private readonly ILogger<RoleManagerController> _logger;
        private readonly IRoleRepository _repository;
        private readonly IConverterHelper _converterHelper;

        public RoleManagerController(ILogger<RoleManagerController> logger,
                                    IRoleRepository repository,
                                    IConverterHelper converterHelper)
        {
            _logger = logger;
            _repository = repository;
            _converterHelper = converterHelper;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] AddUpdateRoleModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Role fields are not valid or fields are missing");
                }
                model.Id = Guid.NewGuid().ToString();
                model.Status = true;
                model.DateCreated = DateTime.UtcNow;
                Role entityModel = _converterHelper.ConvertTo<Role,AddUpdateRoleModel>(model);
                var result = await _repository.CreateRoleAsync(entityModel);
                if (!result)
                {
                    return BadRequest("Role could not be created");
                }
                return Ok("Role created successfully");
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

        [HttpGet("GetRole/{roleName}")]
        public async Task<IActionResult> GetRoleAsync(string roleName)
        {
            try
            {
                if (string.IsNullOrEmpty(roleName))
                {
                    return BadRequest("Role name cannot be empty");
                }
                var result = await _repository.GetRoleByNameAsync(roleName);
                if (result == null)
                {
                    return NotFound($"Role {roleName} is not found");
                }
                return Ok(_converterHelper.ConvertTo<RoleDtoModel,Role>(result));
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

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            try
            {
                var result = await _repository.GetActiveRolesAsync();
                List<RoleDtoModel> rolesFound = _converterHelper.ConvertTo<List<RoleDtoModel>,List<Role>>(result);
                return Ok(rolesFound);
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