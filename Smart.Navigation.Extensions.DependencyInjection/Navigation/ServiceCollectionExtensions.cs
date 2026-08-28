namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    [RequiresUnreferencedCode("AddNavigator uses NavigatorConfig() which relies on reflection-based plugins. Use explicit configuration for AOT-compatible setup.")]
    [RequiresDynamicCode("AddNavigator uses NavigatorConfig() which uses dynamic delegate creation. Use explicit configuration for AOT-compatible setup.")]
    public static IServiceCollection AddNavigator(this IServiceCollection service, Action<IServiceProvider, NavigatorConfig> action)
    {
        service.AddSingleton<INavigator>(p =>
        {
            var config = new NavigatorConfig();

            config.UseActivator(p);

            action(p, config);

            return config.ToNavigator();
        });

        return service;
    }
}
