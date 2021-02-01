using Newtonsoft.Json;
using SauronEye.AllDomain.Domain.Entities.Zabbix;
using SauronEye.AllDomain.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Services.App
{
    public class MockedServiceZabbix : IServiceZabbix
    {
        public Task<List<Event>> GetEvents(string zabbixToken, string id) => null;


        public Task<List<Problem>> GetProblems(string zabbixToken, string id)
        {
            return Task.Run(() =>
            {
                string json = MockReaderHelper.ReadResource("SauronEye.AllDomain.Domain.Mocks.Zabbix.EventList.json");
                var collection = JsonConvert.DeserializeObject<IEnumerable<Problem>>(json);
                return collection.ToList();
            });
        }

        public Task<ZabbixToken> Login(string username, string password) => null;
    }
}
