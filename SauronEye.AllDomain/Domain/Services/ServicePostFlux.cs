using Microsoft.Extensions.Logging;
using SauronEye.AllDomain.Domain.Contracts.Services;
using SauronEye.AllDomain.Domain.Entities;
using SauronEye.AllDomain.Domain.Services.MockedServices;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SauronEye.AllDomain.Domain.Services
{
    public class ServicePostFlux
    {
        private readonly IServiceProblem serviceProblem;
        private readonly ILogger<ServicePostFlux> logger;

        public ServicePostFlux(ILogger<ServicePostFlux> logger)
        {
            serviceProblem = new MockedServiceProblem();
            logger = logger;
        }

        public async Task PostOnFlux()
        {
            var list = await serviceProblem.GetProblems("mocked_token");


            foreach(var problem in list)
            {
                try
                {
                    await PostProblem(problem);
                }
                catch(Exception ex)
                {
                    logger.LogError("Error on post on flux", ex);
                }
            }
        }

        private async Task<string> PostProblem(Problem problem)
        {
            HttpClient client = new HttpClient();
            string resp = null;

            client.BaseAddress = new Uri("https://opti.xpinc.io/zabbix-hml/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api_jsonrpc.php", problem);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                resp = json;
            }

            // return URI of the created resource.
            return resp;
        }

    }
}
