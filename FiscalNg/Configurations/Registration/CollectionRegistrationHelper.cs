using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FiscalNg.Api.Configurations.Registration
{
    /// <summary>
    /// Batch register dependencies
    /// </summary>
    public static class CollectionRegistrationHelper {
        /// <summary>
        /// Automatically register repositories into DI
        /// Registers only those repositories that can be batch registered
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        /// <param name="nameSpace"></param>
        /// <param name="memberContainsName"></param>
        /// <param name="lifetime"></param>
        public static void RegisterCollection(this IServiceCollection services, string assemblyName, string nameSpace = "", 
            string memberContainsName = "", ServiceLifetime lifetime = ServiceLifetime.Scoped) {
            if(string.IsNullOrWhiteSpace(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName), "Assembly name not set");

            var repositoryTypes = Assembly.Load(assemblyName).GetTypes()
                .Where(t => (string.IsNullOrWhiteSpace(nameSpace) ||
                             t.Namespace != null &&
                             t.Namespace.Contains(nameSpace, StringComparison.CurrentCultureIgnoreCase)) &&
                            (string.IsNullOrWhiteSpace(memberContainsName) ||
                             t.FullName != null &&
                             t.FullName.EndsWith(memberContainsName, StringComparison.InvariantCultureIgnoreCase))
                ).ToArray();

            var implementable = repositoryTypes
                .Where(t => t.IsInterface)
                .SelectMany(inter => repositoryTypes.Where(x => x.GetInterfaces().Contains(inter)),
                    (interfaceType, classType) => new {interfaceType, classType});

            foreach (var item in implementable) {
                services.Add(new ServiceDescriptor(item.interfaceType, item.classType, lifetime));
            }
        }
    }
}
