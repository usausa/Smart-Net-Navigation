namespace Smart.Navigation;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNavigator(this IServiceCollection service, Action<NavigatorConfig> action)
    {
        service.AddSingleton<INavigator>(p =>
        {
            var config = new NavigatorConfig();
            action(config);

            config.UseServiceProvider(p);

            return config.ToNavigator();
        });

        return service;
    }
}
