namespace Smart.Navigation
{
    using System;

    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;
    using Smart.Reflection;

    public static class NavigatorConfigExtensions
    {
        public static NavigatorConfig UseProvider<TProvider>(this NavigatorConfig config)
            where TProvider : INavigationProvider
        {
            config.Configure(c =>
            {
                c.RemoveAll<INavigationProvider>();
                c.Add<INavigationProvider, TProvider>();
            });

            return config;
        }

        public static NavigatorConfig UseProvider(this NavigatorConfig config, INavigationProvider provider)
        {
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

        public static NavigatorConfig UseFactory<TFactory>(this NavigatorConfig config)
            where TFactory : IFactory
        {
            config.Configure(c =>
            {
                c.RemoveAll<IFactory>();
                c.Add<IFactory, TFactory>();
            });

            return config;
        }

        public static NavigatorConfig UseFactory(this NavigatorConfig config, IFactory factory)
        {
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

        public static NavigatorConfig UseConverter<TConverter>(this NavigatorConfig config)
            where TConverter : IConverter
        {
            config.Configure(c =>
            {
                c.RemoveAll<IConverter>();
                c.Add<IConverter, TConverter>();
            });

            return config;
        }

        public static NavigatorConfig UseConverter(this NavigatorConfig config, IConverter converter)
        {
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
            return config.UseConverter(new CallbackConverter(callback));
        }

        public static NavigatorConfig AddPlugin<TPlugin>(this NavigatorConfig config)
            where TPlugin : IPlugin
        {
            config.Configure(c => c.Add<IPlugin, TPlugin>());

            return config;
        }

        public static NavigatorConfig AddPlugin(this NavigatorConfig config, IPlugin plugin)
        {
            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }

            config.Configure(c => c.Add(plugin));

            return config;
        }

        public static NavigatorConfig UseActivatorFactory<TActivatorFactory>(this NavigatorConfig config)
            where TActivatorFactory : IActivatorFactory
        {
            config.Configure(c =>
            {
                c.RemoveAll<IActivatorFactory>();
                c.Add<IActivatorFactory, TActivatorFactory>();
            });

            return config;
        }

        public static NavigatorConfig UseActivatorFactory(this NavigatorConfig config, IActivatorFactory activatorFactory)
        {
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

        public static NavigatorConfig UseAccessorFactory<TAccessorFactory>(this NavigatorConfig config)
            where TAccessorFactory : IAccessorFactory
        {
            config.Configure(c =>
            {
                c.RemoveAll<IAccessorFactory>();
                c.Add<IAccessorFactory, TAccessorFactory>();
            });

            return config;
        }

        public static NavigatorConfig UseAccessorFactory(this NavigatorConfig config, IAccessorFactory accessorFactory)
        {
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
