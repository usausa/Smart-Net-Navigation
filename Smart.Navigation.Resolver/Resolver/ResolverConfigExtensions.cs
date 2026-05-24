namespace Smart.Resolver;

using System.Diagnostics.CodeAnalysis;

using Smart.Navigation;
using Smart.Navigation.Components;
using Smart.Resolver.Expressions;
using Smart.Resolver.Scopes;

public static class ResolverConfigExtensions
{
    public static ResolverConfig UsePageContextScope(this ResolverConfig config)
    {
        config.Bind<PageContextStorage>().ToSelf().InSingletonScope();
        config.Bind<PageContextKeyManager>().ToSelf().InSingletonScope();

        return config;
    }

    public static IBindingConstraintWithSyntax InPageContextScope(this IBindingInSyntax syntax, string name)
    {
        return syntax.InScope(_ => new PageContextScope(name));
    }

    [RequiresUnreferencedCode("AddNavigator uses NavigatorConfig() which relies on reflection-based plugins. Use explicit configuration for AOT-compatible setup.")]
    [RequiresDynamicCode("AddNavigator uses NavigatorConfig() which uses dynamic delegate creation. Use explicit configuration for AOT-compatible setup.")]
    public static ResolverConfig AddNavigator(this ResolverConfig config, Action<NavigatorConfig> action)
    {
        config.Bind<INavigator>().ToMethod(resolver =>
        {
            var navigatorConfig = new NavigatorConfig();
            action(navigatorConfig);

            navigatorConfig.UseServiceProvider(resolver);

            return navigatorConfig.ToNavigator();
        }).InSingletonScope();

        return config;
    }
}
