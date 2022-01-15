using secure_lib.Data.Entities.Security;
using secure_lib.Models.BindingModels;

namespace secure_lib.Services.Interfaces.Security
{
    public interface IJwtTokenFactoryService
    {
        TokenModel GenerateToken(User user);
    }
}