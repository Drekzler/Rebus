using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rebus.Bus;
using Rebus.Config;
using System;
using System.Threading;
using System.Threading.Tasks;
using Test.RebusClient.Components.Dependency;
using Test.RebusClient.Components.Models;

namespace Test.RebusClient
{
    public class ClientBackgroundService : BackgroundService
    {
        public ClientBackgroundService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IServiceCollection Service = new ServiceCollection();
        public IConfiguration Configuration { get; }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Service.AddClientDependencies(Configuration);
            using var provider = Service.BuildServiceProvider();
            provider.UseRebus();
            var bus = provider.GetService<IBus>();
            await bus.Subscribe<Messages>();
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
