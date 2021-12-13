using ClientRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using ServerRepository;
using System.Reflection;
using Test.RebusClient.Components.Models;
using Test.RebusClient.EventHandler;
using Test.RebusClient.Repository;

namespace Test.RebusClient.Components.Dependency
{
    public static class Dependencies
    {
        public static IServiceCollection AddClientDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRebus(config => config
                .Transport(t => t.UseSqlServer(new SqlServerTransportOptions(configuration.GetConnectionString("ServerDbContext"), enlistInAmbientTransaction: false), "jon-testing-queue"))
                .Routing(r => r.TypeBased().Map<Messages>("jon-testing-queue"))
                .Subscriptions(s => s.StoreInSqlServer(configuration.GetConnectionString("ServerDbContext"), "subscriptions"))
            );
            services.AutoRegisterHandlersFromAssemblyOf<MessageEventHandler>();
            services.AddClientSharedDependencies(configuration);



            return services;
        }

        public static IServiceCollection AddClientSharedDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ClientDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ClientDbContext"))
                .EnableSensitiveDataLogging());

            services.AddTransient<IClientRebusRepository, ClientRebusRepository>();
            return services;
        }
    }
}

