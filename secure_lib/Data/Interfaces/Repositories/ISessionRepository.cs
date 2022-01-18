using System.Threading.Tasks;
using secure_lib.Data.Entities.Security;

namespace secure_lib.Data.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task<bool> CreateSessionAsync(Session model);
        Task<bool> RegisterTokenSessionAsync(Token token);
        Task<bool> ExistsTokenAsync(Token token);
    }
}