namespace Smart.Navigation;

using System.Reflection;

using Smart.Converter;
using Smart.Navigation.Attributes;
using Smart.Navigation.Components;
using Smart.Navigation.Mappers;
using Smart.Navigation.Plugins;
using Smart.Reflection;

public static class NavigatorConfigExtensions
{
    public static NavigatorConfig UseProvider<TProvider>(this NavigatorConfig config)
        where TProvider : INavigationProvider
    {
        config.Configure(static c =>
        {
            c.RemoveAll<INavigationProvider>();
            c.Add<INavigationProvider, TProvider>();
        });

        return config;
    }

    public static NavigatorConfig UseProvider(this NavigatorConfig config, INavigationProvider provider)
    {
        config.Configure(c =>
        {
            c.RemoveAll<INavigationProvider>();
            c.Add(provider);
        });

        return config;
    }

    public static NavigatorConfig UseViewMapper<TViewMapper>(this NavigatorConfig config)
        where TViewMapper : IViewMapper
    {
        config.Configure(static c =>
        {
            c.RemoveAll<IViewMapper>();
            c.Add<IViewMapper, TViewMapper>();
        });

        return config;
    }

    public static NavigatorConfig UseViewMapper(this NavigatorConfig config, IViewMapper mapper)
    {
        config.Configure(c =>
        {
            c.RemoveAll<IViewMapper>();
            c.Add(mapper);
        });

        return config;
    }

    public static NavigatorConfig UseDirectViewMapper(this NavigatorConfig config, Type baseType)
    {
        config.Configure(c =>
        {
            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(baseType));
        });

        return config.UseViewMapper<DirectViewMapper>();
    }

    public static NavigatorConfig UseDirectViewMapper<TViewBase>(this NavigatorConfig config)
    {
        return config.UseDirectViewMapper(typeof(TViewBase));
    }

    public static NavigatorConfig UseDirectViewMapper(this NavigatorConfig config)
    {
        return config.UseViewMapper<DirectViewMapper>();
    }

    public static NavigatorConfig UseIdViewMapper(this NavigatorConfig config, Action<IIdViewRegister> action)
    {
        config.Configure(c =>
        {
            c.RemoveAll<IdViewMapperOptions>();
            c.Add(new IdViewMapperOptions(action));
        });

        return config.UseViewMapper<IdViewMapper>();
    }

    public static void AutoRegister(this IIdViewRegister register, IEnumerable<Type> types)
    {
        foreach (var type in types)
        {
            foreach (var attr in type.GetTypeInfo().GetCustomAttributes<ViewAttribute>())
            {
                register.Register(attr.Id, type);
            }
        }
    }

    public static void AutoRegister<T>(this IIdViewRegister register, IEnumerable<KeyValuePair<T, Type>> source)
    {
        foreach (var pair in source)
        {
            register.Register(pair.Key!, pair.Value);
        }
    }

    public static NavigatorConfig UsePathViewMapper(this NavigatorConfig config, Action<PathViewMapperOptions> action)
    {
        var options = new PathViewMapperOptions();
        action(options);

        config.Configure(c =>
        {
            c.RemoveAll<PathViewMapperOptions>();
            c.Add(options);
        });

        return config.UseViewMapper<PathViewMapper>();
    }

    public static NavigatorConfig UseTypeConstraint<TTypeConstraint>(this NavigatorConfig config)
        where TTypeConstraint : ITypeConstraint
    {
        config.Configure(static c =>
        {
            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint, TTypeConstraint>();
        });

        return config;
    }

    public static NavigatorConfig UseTypeConstraint(this NavigatorConfig config, ITypeConstraint constraint)
    {
        config.Configure(c =>
        {
            c.RemoveAll<ITypeConstraint>();
            c.Add(constraint);
        });

        return config;
    }

    public static NavigatorConfig UseServiceProvider<TServiceProvider>(this NavigatorConfig config)
        where TServiceProvider : IServiceProvider
    {
        config.Configure(static c =>
        {
            c.RemoveAll<IServiceProvider>();
            c.Add<IServiceProvider, TServiceProvider>();
        });

        return config;
    }

    public static NavigatorConfig UseServiceProvider(this NavigatorConfig config, IServiceProvider serviceProvider)
    {
        config.Configure(c =>
        {
            c.RemoveAll<IServiceProvider>();
            c.Add(serviceProvider);
        });

        return config;
    }

    public static NavigatorConfig UseServiceProvider(this NavigatorConfig config, Func<Type, object?> callback)
    {
        return config.UseServiceProvider(new DelegateServiceProvider(callback));
    }

    public static NavigatorConfig UseConverter<TConverter>(this NavigatorConfig config)
        where TConverter : IConverter
    {
        config.Configure(static c =>
        {
            c.RemoveAll<IConverter>();
            c.Add<IConverter, TConverter>();
        });

        return config;
    }

    public static NavigatorConfig UseConverter(this NavigatorConfig config, IConverter converter)
    {
        config.Configure(c =>
        {
            c.RemoveAll<IConverter>();
            c.Add(converter);
        });

        return config;
    }

    public static NavigatorConfig UseConverter(this NavigatorConfig config, Func<object?, Type, object?> callback)
    {
        return config.UseConverter(new DelegateConverter(callback));
    }

    public static NavigatorConfig UseConverter(this NavigatorConfig config, IObjectConverter converter)
    {
        return config.UseConverter(new SmartConverter(converter));
    }

    public static NavigatorConfig AddPlugin<TPlugin>(this NavigatorConfig config)
        where TPlugin : IPlugin
    {
        config.Configure(static c => c.Add<IPlugin, TPlugin>());

        return config;
    }

    public static NavigatorConfig AddPlugin(this NavigatorConfig config, IPlugin plugin)
    {
        config.Configure(c => c.Add(plugin));

        return config;
    }

    public static NavigatorConfig UseDelegateFactory<TDelegateFactory>(this NavigatorConfig config)
        where TDelegateFactory : IDelegateFactory
    {
        config.Configure(static c =>
        {
            c.RemoveAll<IDelegateFactory>();
            c.Add<IDelegateFactory, TDelegateFactory>();
        });

        return config;
    }

    public static NavigatorConfig UseDelegateFactory(this NavigatorConfig config, IDelegateFactory delegateFactory)
    {
        config.Configure(c =>
        {
            c.RemoveAll<IDelegateFactory>();
            c.Add(delegateFactory);
        });

        return config;
    }

    public static Navigator ToNavigator(this INavigatorConfig config)
    {
        return new(config);
    }
}
