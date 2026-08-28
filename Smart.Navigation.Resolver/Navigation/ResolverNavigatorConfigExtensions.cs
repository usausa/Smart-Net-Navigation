namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Smart.Navigation.Components;
using Smart.Navigation.Plugins.Resolver;
using Smart.Resolver;

public static class ResolverNavigatorConfigExtensions
{
    [RequiresUnreferencedCode("AddResolverPlugin uses ResolverPlugin which relies on reflection. This may not work with trimming.")]
    public static NavigatorConfig AddResolverPlugin(this NavigatorConfig config, IResolver resolver)
    {
        config.AddPlugin(new ResolverPlugin(resolver.Get<PageContextStorage>()));
        return config;
    }
}
