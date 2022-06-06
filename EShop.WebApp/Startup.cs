using EShop.Common;
using EShop.WebApp.Services;
using Man.Dapr.Sidekick;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EShop.WebApp
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
            services.AddRazorPages().AddDapr();
            services.AddDaprClients();
            services.AddScoped<IStateStore, StateStore>();

            var envVar = Environment.GetEnvironmentVariable("DISABLE_SIDEKICK");
            bool.TryParse(envVar, out var sidekickDisabled);

            services.AddDaprSidekick(Configuration, o =>
            {
                o.Sidecar = new DaprSidecarOptions
                {
                    AppId = "webapp",
                    DaprHttpPort = 3551,
                    ComponentsDirectory = "../Dapr/Components",
                    ConfigFile = "../Dapr/Config/config.yaml",
                    Enabled = !sidekickDisabled
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
