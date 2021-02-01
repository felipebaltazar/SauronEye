using Microsoft.Extensions.Configuration;
using SauronEye.AllDomain.Domain.Contracts.Services;
using SauronEye.AllDomain.Domain.Dtos;
using SauronEye.AllDomain.Domain.Services.App;
using SauronEye.AllDomain.Repositories;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Services
{
    public class ServiceProblem : IServiceProblem
    {
        private ServiceZabbix serviceZabbix = null;
        private ServiceUser serviceUser = null;
        private ProblemRepository problemRepo = null;
        private UserRepository userRepo = null;

        public ServiceProblem(IConfiguration config)
        {
            this.serviceZabbix = new ServiceZabbix();
            this.serviceUser = new ServiceUser(config);
            this.problemRepo = new ProblemRepository(config);
            this.userRepo = new UserRepository(config);
        }

        public void GetAndSaveProblems(ConcurrentDictionary<string, LoggedUser> loggedUsers)
        {
            foreach (var user in loggedUsers)
            {
                var task = Task.Factory.StartNew(async () =>
                {
                    var userData = await serviceUser.GetByUsername(user.Key);
                    var list = await serviceZabbix.GetProblems(userData.TokenZabbix, userData.IdZabbix);
                    foreach (var item in list)
                    {
                        var entity = item.GetEntity(userData.Username);
                        this.problemRepo.Save(entity);
                    }
                });
                task.Wait();
            }

        }

        public async Task<List<Entities.Problem>> GetProblems(string token)
        {
            if (string.IsNullOrEmpty(token)) throw new System.Exception("An authorization token required.");
            token = token.Replace("Basic ", "");
            List<Entities.Problem> list = null;

            var userData = await this.userRepo.GetByToken(token);
            if (userData != null)
            {
                list = await problemRepo.GetByUser(userData?.Username);
            }
            return list;
        }
    }
}
