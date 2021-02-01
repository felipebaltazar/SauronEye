using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SauronEye.AllDomain.Domain.Services;
using System.Threading;

namespace SauronEye.MasterRing.Api.Hosted
{
    public class PostPoblemService : BackgroundService
    {
        private readonly ServicePostFlux serviceProblem;
        private readonly ILogger<ServicePostFlux> logger;

        public PostPoblemService(IConfiguration config, ILogger<ServicePostFlux> logger) : base(config)
        {
            serviceProblem = new ServicePostFlux(logger);
        }

        protected override void DoWork(object state)
        {
            serviceProblem.PostOnFlux();

            timer.Change(seconds * 1000, Timeout.Infinite);
        }

    }
}
