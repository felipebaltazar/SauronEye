using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using SauronEye.MasterRing.Api.Model;

namespace SauronEye.MasterRing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PushController: ControllerBase
    {
        
        private readonly ILogger<PushController> logger;

        public PushController(IConfiguration config, ILogger<PushController> logger)
        {
            //this.serviceProblem = new ServiceProblem(config);
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PushSubscription pushSubscription)
        {
            var token = Request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(token)) return NotFound();

            return Ok();
        }

    }
}
