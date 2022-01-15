using System.Collections.Generic;
using System.Threading.Tasks;
using secure_lib.Data.Entities.Security;
using secure_lib.Models.Dto;

namespace secure_lib.Data.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> CreateRoleAsync(Role model);
        Task<Role> GetRoleByNameAsync(string roleName);
        Task<List<Role>> GetActiveRolesAsync();
    }
}