using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using secure_lib.Data.Entities.Security;
using secure_lib.Data.Interfaces.Repositories;

namespace secure_lib.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DataContext _dataContext;

        public SessionRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateSessionAsync(Session model)
        {
            var isRegisteredToken = await RegisterTokenSessionAsync(model.Token);
            if (!isRegisteredToken)
            {
                return false;
            }
            _dataContext.Sessions.Add(model);
            var result = await _dataContext.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> RegisterTokenSessionAsync(Token token)
        {
            bool tokenExists = await ExistsTokenAsync(token);
            if (tokenExists)
            {
                return false;
            }
            _dataContext.Tokens.Add(token);
            var result = await _dataContext.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsTokenAsync(Token token) 
        {
            return await _dataContext.Tokens.Where(t => t.TokenCode == token.TokenCode).FirstOrDefaultAsync() != null;
        }
    }
}