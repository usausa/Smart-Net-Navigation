namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Smart.Navigation.Plugins.Resolver;

public static class ResolverNavigatorConfigExtensions
{
    [RequiresUnreferencedCode("AddResolverPlugin uses ResolverPlugin which relies on reflection. This may not work with trimming.")]
    public static NavigatorConfig AddResolverPlugin(this NavigatorConfig config)
    {
        config.AddPlugin<ResolverPlugin>();
        return config;
    }
}
