using System.Collections.Generic;
using System.Threading.Tasks;
using secure_lib.Data.Entities.Security;

namespace secure_lib.Data.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetActiveUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<bool> CreateUserAsync(User user);
        Task<bool> AssignRoleAsync(User user, Role role);
    }
}