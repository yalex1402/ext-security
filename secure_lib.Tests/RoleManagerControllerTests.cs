using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using secure_lib.Controllers.Security;
using secure_lib.Data.Interfaces.Repositories;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Dto;
using Xunit;

namespace secure_lib.Tests
{
    public class RoleManagerControllerTests
    {
        [Fact]
        public async Task CreateRoleAsync_WithExistingRole_ReturnsBadRequest()
        {
            //Arrange
            AddUpdateRoleModel model = new AddUpdateRoleModel()
            {
                Name = "Admin"
            };
            var repositoryStub = new Mock<IRepository>();
            repositoryStub
                .Setup(repo => repo.CreateRoleAsync(It.IsAny<RoleDtoModel>()))
                .ReturnsAsync((RoleDtoModel)null);
            var loggerStub = new Mock<ILogger<RoleManagerController>>();
            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(map => map.Map<RoleDtoModel>(It.IsAny<AddUpdateRoleModel>())).Returns(new RoleDtoModel());
            var controller = new RoleManagerController(loggerStub.Object,mapperStub.Object,repositoryStub.Object);

            //Act
            var result = await controller.CreateRoleAsync(model);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
