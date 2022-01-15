using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using secure_lib.Controllers.Security;
using secure_lib.Data.Entities.Security;
using secure_lib.Data.Interfaces.Repositories;
using secure_lib.Helpers.Interfaces;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Dto;
using Xunit;

namespace secure_lib.Tests.ControllerTests 
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
            var repositoryStub = new Mock<IRoleRepository>();
            repositoryStub
                .Setup(repo => repo.CreateRoleAsync(It.IsAny<Role>()))
                .ReturnsAsync(false);
            var loggerStub = new Mock<ILogger<RoleManagerController>>();
            var converterStub = new Mock<IConverterHelper>();
            converterStub
                .Setup(conv => conv.ConvertTo<Role,AddUpdateRoleModel>(It.IsAny<AddUpdateRoleModel>()))
                .Returns(new Role());
            var controller = new RoleManagerController(loggerStub.Object,repositoryStub.Object,converterStub.Object);

            //Act
            var result = await controller.CreateRoleAsync(model);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetRoleAsync_WithUnexistingRole_ReturnsNotFound()
        {
            //Arrange
            string roleName = "Developer";
            var repositoryStub = new Mock<IRoleRepository>();
            repositoryStub
                .Setup(repo => repo.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Role)null);
            var loggerStub = new Mock<ILogger<RoleManagerController>>();
            var converterStub = new Mock<IConverterHelper>();
            converterStub
                .Setup(conv => conv.ConvertTo<RoleDtoModel,Role>(It.IsAny<Role>()))
                .Returns(new RoleDtoModel());
            var controller = new RoleManagerController(loggerStub.Object,repositoryStub.Object,converterStub.Object);

            //Act
            var result = await controller.GetRoleAsync(roleName);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetRoleAsync_WithInvalidRoleName_ReturnsBadRequest()
        {
            //Arrange
            string roleName = "";
            var repositoryStub = new Mock<IRoleRepository>();
            repositoryStub
                .Setup(repo => repo.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Role)null);
            var loggerStub = new Mock<ILogger<RoleManagerController>>();
            var converterStub = new Mock<IConverterHelper>();
            converterStub
                .Setup(conv => conv.ConvertTo<RoleDtoModel,Role>(It.IsAny<Role>()))
                .Returns(new RoleDtoModel());
            var controller = new RoleManagerController(loggerStub.Object,repositoryStub.Object,converterStub.Object);

            //Act
            var result = await controller.GetRoleAsync(roleName);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetRoleAsync_WithExistingRole_ReturnsOk()
        {
            //Arrange
            string roleName = "Admin";
            var repositoryStub = new Mock<IRoleRepository>();
            repositoryStub
                .Setup(repo => repo.GetRoleByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role());
            var loggerStub = new Mock<ILogger<RoleManagerController>>();
            var converterStub = new Mock<IConverterHelper>();
            converterStub
                .Setup(conv => conv.ConvertTo<RoleDtoModel,Role>(It.IsAny<Role>()))
                .Returns(new RoleDtoModel());
            var controller = new RoleManagerController(loggerStub.Object,repositoryStub.Object,converterStub.Object);
            
            //Act
            var result = await controller.GetRoleAsync(roleName);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
