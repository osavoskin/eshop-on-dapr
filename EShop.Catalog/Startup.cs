using EShop.Common;
using Man.Dapr.Sidekick;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EShop.Catalog
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddDapr();
            services.AddDaprClients();

            var envVar = Environment.GetEnvironmentVariable("DISABLE_SIDEKICK");
            bool.TryParse(envVar, out var sidekickDisabled);

            services.AddDaprSidekick(Configuration, o =>
            {
                o.Sidecar = new DaprSidecarOptions
                {
                    AppId = "catalog",
                    DaprHttpPort = 3552,
                    ComponentsDirectory = "../Dapr/Components",
                    ConfigFile = "../Dapr/Config/config.yaml",
                    Enabled = !sidekickDisabled
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthorization();
            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
