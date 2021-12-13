using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;

namespace RebusServer.Components.Dependency
{
    public static class ServerDependency
    {
        public static IServiceCollection AddServerDependencies(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddRebus(config => config
                .Transport(t => t.UseSqlServer(new SqlServerTransportOptions(configuration.GetConnectionString("ServerDbContext"), enlistInAmbientTransaction: false), "jon-testing-queue"))
                .Subscriptions(s => s.StoreInSqlServer(configuration.GetConnectionString("ServerDbContext"), "subscriptions"))
            );
            return service;
        }
    }
}
