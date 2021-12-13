using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rebus.Config;
using RebusServer.Components.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RebusServer
{
    public class ServerBackgroundService : BackgroundService
    {

        public ServerBackgroundService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceCollection Service = new ServiceCollection();
        public IConfiguration Configuration { get; }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var provider = Service.BuildServiceProvider();
            Service.AddServerDependencies(Configuration);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
