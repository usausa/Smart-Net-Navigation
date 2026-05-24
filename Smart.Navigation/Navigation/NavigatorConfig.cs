namespace Smart.Navigation;

using System.Diagnostics.CodeAnalysis;

using Smart.ComponentModel;
using Smart.Navigation.Components;
using Smart.Navigation.Mappers;
using Smart.Navigation.Plugins;
using Smart.Navigation.Plugins.Parameter;
using Smart.Navigation.Plugins.Scope;
using Smart.Reflection;

public sealed class NavigatorConfig : INavigatorConfig
{
    private readonly ComponentConfig config = new();

    [RequiresUnreferencedCode("Default NavigatorConfig uses StandardServiceProvider, ParameterPlugin and ScopePlugin which rely on reflection. Use explicit configuration for AOT-compatible setup.")]
    [RequiresDynamicCode("Default NavigatorConfig uses ParameterPlugin and ScopePlugin which use dynamic delegate creation. Use explicit configuration for AOT-compatible setup.")]
    public NavigatorConfig()
    {
        config.Add<IViewMapper, DirectViewMapper>();
        config.Add<IServiceProvider, StandardServiceProvider>();
        config.Add<IConverter, SmartConverter>();
        config.Add<IDelegateFactory>(DelegateFactory.Default);
        config.Add<IPlugin, ParameterPlugin>();
        config.Add<IPlugin, ScopePlugin>();
    }

    public NavigatorConfig Configure(Action<ComponentConfig> action)
    {
        action(config);

        return this;
    }

    ComponentContainer INavigatorConfig.ResolveComponents()
    {
        return config.ToContainer();
    }
}
