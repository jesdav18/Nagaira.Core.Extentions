using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nagaira.WebApi.Utilities.Configurations;

namespace Nagaira.WebApi.Utilities.Extensions
{
    public static class InstallerExtension
    {
        public static void InstallServices<TStartup>(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(TStartup).Assembly.ExportedTypes
                                            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                                            .Select(Activator.CreateInstance).Cast<IInstaller>()
                                            .ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }


    }
}
