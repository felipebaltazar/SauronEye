using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SauronEye.MasterRing.Api.Hosted
{
    // Copyright (c) .NET Foundation. Licensed under the Apache License, Version 2.0.
    /// <summary>
    /// Base class for implementing a long running <see cref="IHostedService"/>.
    /// </summary>
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        protected Timer timer;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        protected readonly int seconds;
        protected int executionCount = 0;
        protected static bool running = false;

        protected BackgroundService(IConfiguration config)
        {
            seconds = config.GetValue<int>("GetZabbixEventsDelay");
        }

        //protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
        protected abstract void DoWork(object state);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {

            //timer = new Timer(DoWork, null, , TimeSpan.FromSeconds(seconds));
            timer = new Timer(DoWork, null, 0, Timeout.Infinite);

            return Task.CompletedTask;
        }

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;

        }

        public virtual void Dispose()
        {
            timer?.Dispose();
        }
    }
}
