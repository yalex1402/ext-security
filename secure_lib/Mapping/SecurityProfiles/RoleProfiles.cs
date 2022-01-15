using AutoMapper;
using secure_lib.Data.Entities.Security;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Dto;

namespace secure_lib.Mapping.SecurityProfiles
{
    public class RoleProfiles : Profile
    {
        public RoleProfiles()
        {
            CreateMap<AddUpdateRoleModel,RoleDtoModel>();
            CreateMap<AddUpdateRoleModel,Role>();
            CreateMap<RoleDtoModel,AddUpdateRoleModel>();
            CreateMap<RoleDtoModel,Role>();
            CreateMap<Role,RoleDtoModel>();
            
        }
    }
}