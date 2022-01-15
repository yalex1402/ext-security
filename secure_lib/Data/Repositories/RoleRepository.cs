using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using secure_lib.Data.Entities.Security;
using secure_lib.Data.Interfaces.Repositories;
using secure_lib.Models.Dto;

namespace secure_lib.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public RoleRepository(DataContext dataContext,
                            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateRoleAsync(Role model)
        {
            var searchUserResult = await GetRoleByNameAsync(model.Name);
            if (searchUserResult != null)
            {
                return false;
            }
            _dataContext.Roles.Add(model);
            int result = await _dataContext.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            var roleFound = await _dataContext.Roles
                                            .Where(r => r.Name == roleName)
                                            .FirstOrDefaultAsync();
            return roleFound;
        }

        public async Task<List<Role>> GetActiveRolesAsync()
        {
            List<Role> roles = await _dataContext.Roles.Where(r => r.Status == true).ToListAsync();
            return roles;
        }
    }
}