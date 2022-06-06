using EShop.Common;
using EShop.Ordering.Actors;
using Man.Dapr.Sidekick;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EShop.Ordering
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
            services.AddActors(options =>
            {
                options.HttpEndpoint = "http://127.0.0.1:3553";
                options.Actors.RegisterActor<OrderProcessorActor>();
            });

            services.AddDaprSidekick(Configuration, o =>
            {
                o.Sidecar = new DaprSidecarOptions
                {
                    AppId = "ordering",
                    DaprHttpPort = 3553,
                    PlacementHostAddress = "127.0.0.1:50005",
                    ComponentsDirectory = "../Dapr/Components",
                    ConfigFile = "../Dapr/Config/config.yaml"
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
                endpoints.MapActorsHandlers();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
