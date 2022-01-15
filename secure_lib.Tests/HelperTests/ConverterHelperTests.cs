using AutoMapper;
using Moq;
using secure_lib.Models.BindingModels;
using secure_lib.Helpers.Conversors;
using secure_lib.Models.Dto;
using Xunit;
using secure_lib.Data.Entities.Security;

namespace secure_lib.Tests.HelperTests 
{
    public class ConverterHelperTests
    {
        [Fact]
        public void ConvertTo_WithBindingMoldel_ReturnsDtoModel()
        {
            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(map => map.Map<RoleDtoModel>(It.IsAny<AddUpdateRoleModel>())).Returns(new RoleDtoModel());
            var helperStub = new ConverterHelper(mapperStub.Object);

            var result = helperStub.ConvertTo<RoleDtoModel,AddUpdateRoleModel>(new AddUpdateRoleModel());

            Assert.IsType<RoleDtoModel>(result);
        }

        [Fact]
        public void ConvertTo_WithDtoMoldel_ReturnsBindingModel()
        {
            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(map => map.Map<AddUpdateRoleModel>(It.IsAny<RoleDtoModel>())).Returns(new AddUpdateRoleModel());
            var helperStub = new ConverterHelper(mapperStub.Object);

            var result = helperStub.ConvertTo<AddUpdateRoleModel,RoleDtoModel>(new RoleDtoModel());

            Assert.IsType<AddUpdateRoleModel>(result);
        }

        [Fact]
        public void ConvertTo_WithDtoMoldel_ReturnsEntity()
        {
            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(map => map.Map<Role>(It.IsAny<RoleDtoModel>())).Returns(new Role());
            var helperStub = new ConverterHelper(mapperStub.Object);

            var result = helperStub.ConvertTo<Role,RoleDtoModel>(new RoleDtoModel());

            Assert.IsType<Role>(result);
        }

        [Fact]
        public void ConvertTo_WithWrongParameters_ReturnsNull()
        {
            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(map => map.Map<Role>(It.IsAny<RoleDtoModel>())).Returns(new Role());
            var helperStub = new ConverterHelper(mapperStub.Object);

            var result = helperStub.ConvertTo<AddUpdateRoleModel,RoleDtoModel>(new RoleDtoModel());

            Assert.Equal(null, result);
        }
    }
}