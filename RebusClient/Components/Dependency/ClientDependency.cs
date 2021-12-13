using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using RebusClient.Models;

namespace RebusClient.Components.Dependency
{
    public static class ClientDependency
    {
        public static IServiceCollection AddClientDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRebus(config => config
                .Transport(t => t.UseSqlServer(new SqlServerTransportOptions(configuration.GetConnectionString("localDb"), enlistInAmbientTransaction: false), "jon-testing-queue"))
                .Routing(r => r.TypeBased().Map<Messages>("jon-testing-queue"))
            );

            return services;
        }

    }
}
