using EShop.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EShop.Ordering
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddDapr();
            services.AddDaprClients();
            services.AddActors(options =>
            {
                // Register actor types and configure actor settings
                //options.Actors.RegisterActor<MyActor>();

                // Configure default settings
                options.ActorIdleTimeout = TimeSpan.FromMinutes(10);
                options.ActorScanInterval = TimeSpan.FromSeconds(35);
                options.DrainOngoingCallTimeout = TimeSpan.FromSeconds(35);
                options.DrainRebalancedActors = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapActorsHandlers();
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers();
            });
        }
    }
}
