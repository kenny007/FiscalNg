using Microsoft.Extensions.DependencyInjection;

namespace FiscalNg.Api.Configurations.Registration
{
    /// <summary>
    /// Registration of services and repositories
    /// </summary>
    public static class ServiceRegistrar {
        /// <summary>
        /// Extension method to register services and repositories
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServicesAndRepositories(this IServiceCollection services) {
            services.RegisterCollection("","","");
            // Here we add those assemblyName and namespace
        }
    }
}
