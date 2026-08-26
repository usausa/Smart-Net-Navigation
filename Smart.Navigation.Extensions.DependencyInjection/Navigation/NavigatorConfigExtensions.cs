namespace Smart.Navigation;

public static class NavigatorConfigExtensions
{
    public static NavigatorConfig UseActivatorUtilities(this NavigatorConfig config, IServiceProvider provider)
    {
        config.UseServiceProvider(provider);
        config.UseActivator(new ActivatorUtilitiesActivator(provider));

        return config;
    }
}
