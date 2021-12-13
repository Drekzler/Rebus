using ClientRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rebus.Config;
using RebusServer;
using RebusServer.Components.Dependency;
using ServerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.RebusClient;
using Test.RebusClient.Components.Dependency;

namespace Jon.Rebus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHostedService<ClientBackgroundService>();
            services.AddServerDependencies(Configuration);
            
            services.AddDbContext<ClientDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ClientDbContext"))
            .EnableSensitiveDataLogging());

            services.AddDbContext<ServerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ServerDbContext"))
            .EnableSensitiveDataLogging());


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jon.Rebus", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jon.Rebus v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.ApplicationServices.UseRebus();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
