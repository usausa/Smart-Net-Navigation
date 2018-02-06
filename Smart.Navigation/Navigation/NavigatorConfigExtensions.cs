namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Smart.Converter;
    using Smart.Navigation.Attributes;
    using Smart.Navigation.Components;
    using Smart.Navigation.Descriptors;
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

        public static NavigatorConfig UseViewMapper<TMapper>(this NavigatorConfig config)
            where TMapper : IViewMapper
        {
            config.Configure(c =>
            {
                c.RemoveAll<IViewMapper>();
                c.Add<IViewMapper, TMapper>();
            });

            return config;
        }

        public static NavigatorConfig UseViewMapper(this NavigatorConfig config, IViewMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            config.Configure(c =>
            {
                c.RemoveAll<IViewMapper>();
                c.Add(mapper);
            });

            return config;
        }

        public static NavigatorConfig UseIdMapper(this NavigatorConfig config, Action<IViewRegister> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var mapper = new IdViewMapper();
            action(mapper);

            return config.UseViewMapper(mapper);
        }

        public static void AutoRegister(this IViewRegister register, IEnumerable<Type> types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            foreach (var type in types)
            {
                foreach (var attr in type.GetTypeInfo().GetCustomAttributes<ViewAttribute>())
                {
                    register.Register(attr.Id ?? type, type);
                }
            }
        }

        public static NavigatorConfig UseDirectMapper(this NavigatorConfig config, Type baseType)
        {
            if (baseType != null)
            {
                config.Configure(c =>
                {
                    c.RemoveAll<IViewMapper>();
                    c.Add<DirectViewMapper>();
                });
            }

            return config.UseViewMapper<DirectViewMapper>();
        }

        public static NavigatorConfig UseDirectMapper(this NavigatorConfig config)
        {
            return config.UseDirectMapper(null);
        }

        public static NavigatorConfig UseActivator<TActivator>(this NavigatorConfig config)
            where TActivator : IActivator
        {
            config.Configure(c =>
            {
                c.RemoveAll<IActivator>();
                c.Add<TActivator>();
            });

            return config;
        }

        public static NavigatorConfig UseActivator(this NavigatorConfig config, IActivator activator)
        {
            if (activator == null)
            {
                throw new ArgumentNullException(nameof(activator));
            }

            config.Configure(c =>
            {
                c.RemoveAll<IActivator>();
                c.Add(activator);
            });

            return config;
        }

        public static NavigatorConfig UseActivator(this NavigatorConfig config, Func<Type, object> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return config.UseActivator(new CallbackActivator(callback));
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

        public static NavigatorConfig UseConverter(this NavigatorConfig config, IObjectConverter converter)
        {
            return config.UseConverter(new SmartConverter(converter));
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

        public static NavigatorConfig UseDelegateFactory<TDelegateFactory>(this NavigatorConfig config)
            where TDelegateFactory : IDelegateFactory
        {
            config.Configure(c =>
            {
                c.RemoveAll<IDelegateFactory>();
                c.Add<IDelegateFactory, TDelegateFactory>();
            });

            return config;
        }

        public static NavigatorConfig UseDelegateFactory(this NavigatorConfig config, IDelegateFactory delegateFactory)
        {
            if (delegateFactory == null)
            {
                throw new ArgumentNullException(nameof(delegateFactory));
            }

            config.Configure(c =>
            {
                c.RemoveAll<IDelegateFactory>();
                c.Add(delegateFactory);
            });

            return config;
        }

        public static Navigator ToNavigator(this INavigatorConfig config)
        {
            return new Navigator(config);
        }
    }
}
