using SauronEye.AllDomain.Domain.Contracts.Services;
using SauronEye.AllDomain.Domain.Dtos;
using SauronEye.AllDomain.Domain.Entities;
using SauronEye.AllDomain.Domain.Services.App;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Services.MockedServices
{
    public class MockedServiceProblem : IServiceProblem
    {
        public void GetAndSaveProblems(ConcurrentDictionary<string, LoggedUser> loggedUsers)
        {
            
        }

        public async Task<List<Problem>> GetProblems(string token)
        {
            var zabbixProblems = await new MockedServiceZabbix().GetProblems("mock_token", "mock_id");
            return zabbixProblems.Select(zp => zp.GetEntity("mock_username")).ToList();
        }
    }
}
