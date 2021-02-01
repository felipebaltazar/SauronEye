using System.Threading.Tasks;
using Saruman.Interfaces.Services;

namespace Saruman.Services
{
    public class AuthService : IAuthService
    {
        public bool IsAuthenticated { get; private set; }

        public async Task<bool> LoginAsync(string username, string password)
        {
            await Task.Delay(800);
            IsAuthenticated = AuthenticateUser(username, password);
            return IsAuthenticated;
        }

        private static bool AuthenticateUser(string username, string password)
            => !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
    }
}
