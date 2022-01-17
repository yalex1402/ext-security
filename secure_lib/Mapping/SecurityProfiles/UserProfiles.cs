using AutoMapper;
using secure_lib.Data.Entities.Security;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Dto;

namespace secure_lib.Mapping.SecurityProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<AddUserModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<AddUserModel, UserDtoModel>();
            CreateMap<UserDtoModel, AddUserModel>();
            CreateMap<UserDtoModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<User, AddUserModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));
            CreateMap<User, UserDtoModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));
        }
    }
}