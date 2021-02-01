using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SauronEye.AllDomain.Domain.Contracts.Services;
using SauronEye.AllDomain.Domain.Services;
using SauronEye.AllDomain.Domain.Services.MockedServices;
using System.Threading.Tasks;

namespace SauronEye.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly IServiceProblem serviceProblem;
        private readonly ILogger<IncidentController> logger;

        public IncidentController(IConfiguration config, ILogger<IncidentController> logger)
        {
            this.serviceProblem = new MockedServiceProblem();
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var token = Request.Headers[HeaderNames.Authorization];
            //if (string.IsNullOrEmpty(token)) return NotFound();

            return Ok(await serviceProblem.GetProblems("mocked_token"));
        }
    }
}
