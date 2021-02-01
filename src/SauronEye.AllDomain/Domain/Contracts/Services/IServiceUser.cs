using SauronEye.AllDomain.Domain.Entities;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Contracts.Services
{
    public interface IServiceUser : IServiceCrud<User>
    {
        Task<User> GetByUsername(string username);
        Task<User> GetByLogin(string username, string password);
    }
}
