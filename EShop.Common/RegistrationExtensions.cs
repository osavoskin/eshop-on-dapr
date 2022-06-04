using EShop.Common.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Common
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddDaprClients(this IServiceCollection services)
        {
            services.AddScoped<ICatalogClient, CatalogClient>();
            return services;
        }
    }
}
