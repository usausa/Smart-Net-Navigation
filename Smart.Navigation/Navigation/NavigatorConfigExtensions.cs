namespace Smart.Navigation
{
    using System;

    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;
    using Smart.Reflection;

    public static class NavigatorConfigExtensions
    {
        public static NavigatorConfig UseProvider(this NavigatorConfig config, INavigationProvider provider)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            config.Configure(c =>
            {
                c.RemoveAll<INavigationProvider>();
                c.Add(provider);
            });

            return config;
        }

        public static NavigatorConfig UseFactory(this NavigatorConfig config, IFactory factory)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            config.Configure(c =>
            {
                c.RemoveAll<IFactory>();
                c.Add(factory);
            });

            return config;
        }

        public static NavigatorConfig UseFactory(this NavigatorConfig config, Func<Type, object> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return config.UseFactory(new CallbackFactory(callback));
        }

        public static NavigatorConfig UseConverter(this NavigatorConfig config, IConverter converter)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            config.Configure(c =>
            {
                c.RemoveAll<IConverter>();
                c.Add(converter);
            });

            return config;
        }

        public static NavigatorConfig UseConverter(this NavigatorConfig config, Func<object, Type, object> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return config.UseConverter(new CallbackConverter(callback));
        }

        public static NavigatorConfig AddPlugin(this NavigatorConfig config, IPlugin plugin)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }

            config.Configure(c => c.Add(plugin));

            return config;
        }

        public static NavigatorConfig UseActivatorFactory(this NavigatorConfig config, IActivatorFactory activatorFactory)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (activatorFactory == null)
            {
                throw new ArgumentNullException(nameof(activatorFactory));
            }

            config.Configure(c =>
            {
                c.RemoveAll<IActivatorFactory>();
                c.Add(activatorFactory);
            });

            return config;
        }

        public static NavigatorConfig UseAccessorFactory(this NavigatorConfig config, IAccessorFactory accessorFactory)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (accessorFactory == null)
            {
                throw new ArgumentNullException(nameof(accessorFactory));
            }

            config.Configure(c =>
            {
                c.RemoveAll<IAccessorFactory>();
                c.Add(accessorFactory);
            });

            return config;
        }

        public static Navigator ToNavigator(this INavigatorConfig config)
        {
            return new Navigator(config);
        }
    }
}
