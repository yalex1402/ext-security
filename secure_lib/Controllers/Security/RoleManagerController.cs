using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using secure_lib.Data.Interfaces.Repositories;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Dto;

namespace secure_lib.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleManagerController : ControllerBase
    {
        private readonly ILogger<RoleManagerController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public RoleManagerController(ILogger<RoleManagerController> logger,
                                    IMapper mapper,
                                    IRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
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
                var result = await _repository.CreateRoleAsync(_mapper.Map<RoleDtoModel>(model));
                if (result == null)
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
    }
}