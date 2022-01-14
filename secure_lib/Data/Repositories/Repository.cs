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
    public class Repository : IRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Repository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<RoleDtoModel> CreateRoleAsync(RoleDtoModel model)
        {
            var searchUserResult = await GetRoleByNameAsync(model.Name);
            if (searchUserResult != null)
            {
                return null;
            }
            _dataContext.Roles.Add(_mapper.Map<Role>(model));
            int result = await _dataContext.SaveChangesAsync();
            if (result != 0)
            {
                return model;
            }
            return null;
        }

        public async Task<RoleDtoModel> GetRoleByNameAsync(string roleName)
        {
            var roleFound = await _dataContext.Roles
                                            .Where(r => r.Name == roleName)
                                            .FirstOrDefaultAsync();
            return _mapper.Map<RoleDtoModel>(roleFound);
        }

        public async Task<List<RoleDtoModel>> GetActiveRolesAsync()
        {
            List<Role> roles = await _dataContext.Roles.Where(r => r.Status == true).ToListAsync();
            return _mapper.Map<List<RoleDtoModel>>(roles);
        }
    }
}