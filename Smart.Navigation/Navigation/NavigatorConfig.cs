namespace Smart.Navigation
{
    using System;

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

        public NavigatorConfig()
        {
            config.Add<IViewMapper, DirectViewMapper>();
            config.Add<IActivator, StandardActivator>();
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
}
