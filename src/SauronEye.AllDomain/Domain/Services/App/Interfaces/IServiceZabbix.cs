using SauronEye.AllDomain.Domain.Entities.Zabbix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Services.App
{
    public interface IServiceZabbix
    {
        Task<List<Event>> GetEvents(string zabbixToken, string id);
        Task<List<Problem>> GetProblems(string zabbixToken, string id);
        Task<ZabbixToken> Login(string username, string password);
    }
}