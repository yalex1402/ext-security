using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<bool> CreateRoleAsync(RoleDtoModel model)
        {
            _dataContext.Roles.Add(_mapper.Map<Role>(model));
            int result = await _dataContext.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }
    }
}