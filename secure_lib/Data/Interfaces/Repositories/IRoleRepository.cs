using System.Collections.Generic;
using System.Threading.Tasks;
using secure_lib.Models.Dto;

namespace secure_lib.Data.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<RoleDtoModel> CreateRoleAsync(RoleDtoModel model);
        Task<RoleDtoModel> GetRoleByNameAsync(string roleName);
        Task<List<RoleDtoModel>> GetActiveRolesAsync();
    }
}