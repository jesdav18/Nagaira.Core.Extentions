using Microsoft.Extensions.DependencyInjection;
using Nagaira.DataLayer.Core.Standard;

namespace Nagaira.DataLayer.Core.Extentions
{
    public static class GenericUnitOfWorkBuilderDependencyInjectionExtension
    {
        public static void AddUnitOfWorkBuilder<TSelector>(this IServiceCollection serviceProvider, Action<IUnitOfWorkConfigurationBuilder<TSelector>> configure)
        {
            serviceProvider.AddScoped<IUnitOfWorkBuilder<TSelector>>(resolver =>
            {
                GenericUnitOfWorkBuilder<TSelector> myUnitOfWorkBuilder = new GenericUnitOfWorkBuilder<TSelector>(resolver);
                configure.Invoke(myUnitOfWorkBuilder);
                return myUnitOfWorkBuilder;
            });
        }
        public static void AddTransientUnitOfWorkBuilder<TSelector>(this IServiceCollection serviceProvider, Action<IUnitOfWorkConfigurationBuilder<TSelector>> configure)
        {
            serviceProvider.AddTransient<IUnitOfWorkBuilder<TSelector>>(resolver =>
            {
                GenericUnitOfWorkBuilder<TSelector> myUnitOfWorkBuilder = new GenericUnitOfWorkBuilder<TSelector>(resolver);
                configure.Invoke(myUnitOfWorkBuilder);
                return myUnitOfWorkBuilder;
            });
        }
    }
}
