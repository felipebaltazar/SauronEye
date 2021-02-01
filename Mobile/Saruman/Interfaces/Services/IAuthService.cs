using System.Threading.Tasks;

namespace Saruman.Interfaces.Services
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }

        Task<bool> LoginAsync(string username, string password);
    }
}