namespace Smart.Navigation;

using Smart.Navigation.Plugins.Resolver;

public static class ResolverNavigatorConfigExtensions
{
    public static NavigatorConfig UseResolverPlugin(this NavigatorConfig config)
    {
        config.AddPlugin<ResolverPlugin>();
        return config;
    }
}
