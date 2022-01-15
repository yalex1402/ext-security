using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using secure_lib.Data.Entities.Security;
using secure_lib.Data.Interfaces.Repositories;

namespace secure_lib.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<User>> GetActiveUsersAsync()
        {
            return await _dataContext.Users.Where(usr => usr.Status).ToListAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dataContext.Users.Where(usr => usr.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _dataContext.Users.Where(usr => usr.UserName == userName).FirstOrDefaultAsync();
        }
    }
}