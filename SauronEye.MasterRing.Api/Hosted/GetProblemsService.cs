using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SauronEye.AllDomain.Domain.Services;
using System.Threading;

namespace SauronEye.MasterRing.Api.Hosted
{
    public class GetProblemsService: BackgroundService
    {
        private readonly IConfiguration config;
        private readonly ILogger<GetProblemsService> logger;
        private readonly ServiceProblem serviceProblem;
        private int executionCount = 0;

        public GetProblemsService(IConfiguration config, ILogger<GetProblemsService> logger): base(config)
        {
            this.logger = logger;
            this.config = config;
            serviceProblem = new ServiceProblem(config);
        }

        protected override void DoWork(object state)
        {
            serviceProblem.GetAndSaveProblems(Startup.LoggedUsers);

            timer.Change(seconds * 1000, Timeout.Infinite);

            logger.LogInformation(
                "Scoped Processing Service is working. Count: {Count}", executionCount);

        }

    }
}
