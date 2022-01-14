using System.Threading.Tasks;
using secure_lib.Models.Dto;

namespace secure_lib.Data.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> CreateRoleAsync(RoleDtoModel model);
    }
}