using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ImageResizerService.Properties
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> Logger;
        private readonly IServiceScopeFactory ServiceScope;
        private Timer Timer;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScope)
        {
            Logger = logger;
            ServiceScope = serviceScope;
        }
        public Worker()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
        
        private async Task DoWork()
        {

            var scope = ServiceScope.CreateScope();
            

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Timed Hosted Service is stopping.");

            Timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
