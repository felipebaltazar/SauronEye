using Microsoft.Extensions.Configuration;
using SauronEye.AllDomain.Domain.Contracts.Services;
using SauronEye.AllDomain.Domain.Entities;
using SauronEye.AllDomain.Repositories;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Services
{
    public class ServiceUser : IServiceUser
    {
        private UserRepository userRepo;

        public ServiceUser(IConfiguration config)
        {
            userRepo = new UserRepository(config);
        }

        public async Task<User> GetByLogin(string username, string password)
        {
            return await userRepo.GetByLogin(username, password);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await userRepo.GetByUsername(username);
        }

        public async Task Save(User item)
        {
            await userRepo.Save(item);
        }
    }


}
