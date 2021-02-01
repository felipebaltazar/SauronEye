using Newtonsoft.Json;
using SauronEye.AllDomain.Domain.Entities.Zabbix;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Services.App
{
    public class ServiceZabbix : IServiceZabbix
    {
        public async Task<ZabbixToken> Login(string username, string password)
        {
            HttpClient client = new HttpClient();
            ZabbixToken resp = null;

            client.BaseAddress = new Uri("https://opti.xpinc.io/zabbix-hml/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var p = new ParamAccess();
            var loginZ = new Caller();
            loginZ.@params = p;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api_jsonrpc.php", loginZ);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic zabbix = JsonConvert.DeserializeObject(json);
                resp = new ZabbixToken()
                {
                    Id = zabbix.id,
                    Token = zabbix.result
                };
            }

            // return URI of the created resource.
            return resp;
        }

        public async Task<List<Event>> GetEvents(string zabbixToken, string id)
        {
            HttpClient client = new HttpClient();
            List<Event> resp = null;

            client.BaseAddress = new Uri("https://opti.xpinc.io/zabbix-hml/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var p = new ParamGetEvents();
            var caller = new Caller();

            caller.method = "event.get";
            caller.auth = zabbixToken;
            caller.id = int.Parse(id);
            caller.@params = p;

            var response = await client.PostAsJsonAsync("api_jsonrpc.php", caller);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic zabbix = JsonConvert.DeserializeObject(json);

                resp = new List<Event>();
                foreach (dynamic d in zabbix.results)
                {
                    resp.Add((Event)d);
                }
            }

            return resp;
        }

        public async Task<List<Problem>> GetProblems(string zabbixToken, string id)
        {
            HttpClient client = new HttpClient();
            List<Problem> resp = null;

            client.BaseAddress = new Uri("https://opti.xpinc.io/zabbix-hml/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var p = new ParamProblems();
            var caller = new Caller();

            caller.method = "problem.get";
            caller.auth = zabbixToken;
            caller.id = int.Parse(id);
            caller.@params = p;

            var response = await client.PostAsJsonAsync("api_jsonrpc.php", caller);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic zabbix = JsonConvert.DeserializeObject(json);
                resp = new List<Problem>();
                foreach (dynamic d in zabbix.result)
                {
                    string jsonItem = JsonConvert.SerializeObject(d);
                    var pr = JsonConvert.DeserializeObject<Problem>(jsonItem);
                    resp.Add(pr);
                }
            }

            // return URI of the created resource.
            return resp;
        }
    }
}
