namespace Smart.Navigation
{
    using System;

    using Smart.ComponentModel;
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;
    using Smart.Navigation.Plugins.Parameter;
    using Smart.Navigation.Plugins.Scope;
    using Smart.Reflection;

    public sealed class NavigatorConfig : INavigatorConfig
    {
        private readonly ComponentConfig config = new ComponentConfig();

        public NavigatorConfig()
        {
            config.Add<IFactory, StandardFactory>();
            config.Add<IConverter, SmartConverter>();
            config.Add<IActivatorFactory>(TypeMetadataFactory.Default);
            config.Add<IAccessorFactory>(TypeMetadataFactory.Default);
            config.Add<IArrayOperatorFactory>(TypeMetadataFactory.Default);
            config.Add<IPlugin, ParameterPlugin>();
            config.Add<IPlugin, ScopePlugin>();
        }

        public void Configure(Action<ComponentConfig> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(config);
        }

        public ComponentContainer ResolveComponents()
        {
            return config.ToContainer();
        }
    }
}
