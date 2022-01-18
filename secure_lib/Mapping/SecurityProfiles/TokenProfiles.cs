using AutoMapper;
using secure_lib.Data.Entities.Security;
using secure_lib.Models.BindingModels;
using secure_lib.Models.Dto;

namespace secure_lib.Mapping.SecurityProfiles
{
    public class TokenProfiles : Profile
    {
        public TokenProfiles()
        {
            CreateMap<TokenModel, Token>()
                .ForMember(dest => dest.TokenCode, opt => opt.MapFrom(src => src.TokenGenerated));
            CreateMap<TokenModel, TokenDtoModel>();
            CreateMap<TokenDtoModel, Token>()
                .ForMember(dest => dest.TokenCode, opt => opt.MapFrom(src => src.TokenGenerated));
            CreateMap<TokenDtoModel, TokenModel>();
            CreateMap<Token, TokenModel>()
                .ForMember(dest => dest.TokenGenerated, opt => opt.MapFrom(src => src.TokenCode));
            CreateMap<Token, TokenDtoModel>()
                .ForMember(dest => dest.TokenGenerated, opt => opt.MapFrom(src => src.TokenCode));
        }
    }
}