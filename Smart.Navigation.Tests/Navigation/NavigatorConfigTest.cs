namespace Smart.Navigation
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Smart.Converter;
    using Smart.Mock;
    using Smart.Navigation.Components;
    using Smart.Navigation.Mappers;
    using Smart.Navigation.Plugins;
    using Smart.Reflection;

    using Xunit;

    public static class NavigatorConfigTest
    {
        // ------------------------------------------------------------
        // Provider
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigUseProviderByInterface()
        {
            var config = new NavigatorConfig()
                .UseProvider<MockFormNavigationProvider>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.NotNull(components.TryGet<INavigationProvider>());
        }

        [Fact]
        public static void ConfigUseProviderByInstance()
        {
            var config = new NavigatorConfig()
                .UseProvider(new MockFormNavigationProvider());

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.NotNull(components.TryGet<INavigationProvider>());
        }

        [Fact]
        public static void ConfigUseProviderByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseProvider(null));
        }

        // ------------------------------------------------------------
        // ViewMapper
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigUseViewMapperByInterface()
        {
            var config = new NavigatorConfig()
                .UseViewMapper<DummyViewMapper>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var viewMapper = components.Get<IViewMapper>();
            Assert.NotNull(viewMapper);
        }

        [Fact]
        public static void ConfigUseViewMapperByInstance()
        {
            var config = new NavigatorConfig()
                .UseViewMapper(new DummyViewMapper());

            var components = ((INavigatorConfig)config).ResolveComponents();

            var viewMapper = components.Get<IViewMapper>();
            Assert.NotNull(viewMapper);
        }

        [Fact]
        public static void ConfigUseViewMapperFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseViewMapper(null));
        }

        [Fact]
        public static void ConfigUseIdViewMapperFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseIdViewMapper(null));
        }

        [Fact]
        public static void ConfigUseIdViewMapperAutoRegisterFailed()
        {
            Assert.Throws<TargetInvocationException>(
                () => new NavigatorConfig().UseMockFormProvider().UseIdViewMapper(r => r.AutoRegister(null)).ToNavigator());
        }

        [Fact]
        public static void ConfigUsePathViewMapperFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UsePathViewMapper(null));
        }

        // ------------------------------------------------------------
        // TypeConstraint
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigUseTypeConstraintByInterface()
        {
            var config = new NavigatorConfig()
                .UseTypeConstraint<DummyTypeConstraint>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var activator = components.TryGet<ITypeConstraint>();
            Assert.NotNull(activator);
        }

        [Fact]
        public static void ConfigUseTypeConstraintByInstance()
        {
            var config = new NavigatorConfig()
                .UseTypeConstraint(new DummyTypeConstraint());

            var components = ((INavigatorConfig)config).ResolveComponents();

            var activator = components.TryGet<ITypeConstraint>();
            Assert.NotNull(activator);
        }

        [Fact]
        public static void ConfigUseTypeConstraintByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseTypeConstraint(null));
        }

        // ------------------------------------------------------------
        // Activator
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigUseActivatorByInterface()
        {
            var config = new NavigatorConfig()
                .UseActivator<StandardActivator>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var activator = components.TryGet<IActivator>();
            Assert.NotNull(activator);
            Assert.NotNull(activator.Resolve(typeof(Model)));
        }

        [Fact]
        public static void ConfigUseActivatorByInstance()
        {
            var config = new NavigatorConfig()
                .UseActivator(new CallbackActivator(Activator.CreateInstance));

            var components = ((INavigatorConfig)config).ResolveComponents();

            var activator = components.TryGet<IActivator>();
            Assert.NotNull(activator);
            Assert.NotNull(activator.Resolve(typeof(Model)));
        }

        [Fact]
        public static void ConfigUseActivatorByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseActivator((IActivator)null));
        }

        [Fact]
        public static void ConfigUseActivatorByCallback()
        {
            var config = new NavigatorConfig()
                .UseActivator(Activator.CreateInstance);

            var components = ((INavigatorConfig)config).ResolveComponents();

            var activator = components.TryGet<IActivator>();
            Assert.NotNull(activator);
            Assert.NotNull(activator.Resolve(typeof(Model)));
        }

        [Fact]
        public static void ConfigUseActivatorByCallbackFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseActivator((Func<Type, object>)null));
        }

        // ------------------------------------------------------------
        // Converter
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigUseConverterByInterface()
        {
            var config = new NavigatorConfig()
                .UseConverter<StandardConverter>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var converter = components.TryGet<IConverter>();
            Assert.NotNull(converter);
            Assert.Equal(1, converter.Convert("1", typeof(int)));
        }

        [Fact]
        public static void ConfigUseConverterByInstance()
        {
            var config = new NavigatorConfig()
                .UseConverter(new StandardConverter());

            var components = ((INavigatorConfig)config).ResolveComponents();

            var converter = components.TryGet<IConverter>();
            Assert.NotNull(converter);
            Assert.Equal(1, converter.Convert("1", typeof(int)));
        }

        [Fact]
        public static void ConfigUseConverterByObjectConverter()
        {
            var config = new NavigatorConfig()
                .UseConverter(ObjectConverter.Default);

            var components = ((INavigatorConfig)config).ResolveComponents();

            var converter = components.TryGet<IConverter>();
            Assert.NotNull(converter);
            Assert.Equal(1, converter.Convert("1", typeof(int)));
        }

        [Fact]
        public static void ConfigUseConverterByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseConverter((IConverter)null));
        }

        [Fact]
        public static void ConfigUseConverterByCallback()
        {
            var config = new NavigatorConfig()
                .UseConverter(Convert.ChangeType);

            var components = ((INavigatorConfig)config).ResolveComponents();

            var converter = components.TryGet<IConverter>();
            Assert.NotNull(converter);
            Assert.Equal(1, converter.Convert("1", typeof(int)));
        }

        [Fact]
        public static void ConfigUseConverterByCallbackFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseConverter((IConverter)null));
        }

        // ------------------------------------------------------------
        // Plugin
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigAddPluginByInterface()
        {
            var config = new NavigatorConfig()
                .AddPlugin<DummyPlugin>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var plugin = components.GetAll<IPlugin>().FirstOrDefault(x => x.GetType() == typeof(DummyPlugin));
            Assert.NotNull(plugin);
        }

        [Fact]
        public static void ConfigAddPluginByInstance()
        {
            var config = new NavigatorConfig()
                .AddPlugin(new DummyPlugin());

            var components = ((INavigatorConfig)config).ResolveComponents();

            var plugin = components.GetAll<IPlugin>().FirstOrDefault(x => x.GetType() == typeof(DummyPlugin));
            Assert.NotNull(plugin);
        }

        [Fact]
        public static void ConfigAddPluginByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().AddPlugin(null));
        }

        // ------------------------------------------------------------
        // IDelegateFactory
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigUseAccessorByInterface()
        {
            var config = new NavigatorConfig()
                .UseDelegateFactory<DummyDelegateFactory>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var delegateFactory = components.Get<IDelegateFactory>();
            Assert.NotNull(delegateFactory);
        }

        [Fact]
        public static void ConfigUseAccessorByInstance()
        {
            var config = new NavigatorConfig()
                .UseDelegateFactory(new DummyDelegateFactory());

            var components = ((INavigatorConfig)config).ResolveComponents();

            var delegateFactory = components.Get<IDelegateFactory>();
            Assert.NotNull(delegateFactory);
        }

        [Fact]
        public static void ConfigUseAccessorByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseDelegateFactory(null));
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigFailed()
        {
            var config = new NavigatorConfig();

            Assert.Throws<ArgumentNullException>(() => config.Configure(null));
        }

        // ------------------------------------------------------------
        // utilitity
        // ------------------------------------------------------------

        public class Model
        {
        }

        public class DummyViewMapper : IViewMapper
        {
            public ViewDescriptor FindDescriptor(object id)
            {
                return null;
            }

            public void CurrentUpdated(object id)
            {
            }
        }

        public class DummyTypeConstraint : ITypeConstraint
        {
            public bool IsValidType(Type type)
            {
                return true;
            }
        }

        public class DummyPlugin : PluginBase
        {
        }

        public class DummyDelegateFactory : IDelegateFactory
        {
            public bool IsCodegenRequired => false;

            public Func<int, Array> CreateArrayAllocator(Type type)
            {
                return null;
            }

            public Func<object> CreateFactory(Type type)
            {
                return null;
            }

            public Func<object[], object> CreateFactory(Type type, Type[] argumentTypes)
            {
                return null;
            }

            public Func<object[], object> CreateFactory(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object> CreateFactory0(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object> CreateFactory1(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object> CreateFactory2(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object> CreateFactory3(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object> CreateFactory4(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object> CreateFactory5(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object> CreateFactory6(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object> CreateFactory7(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object> CreateFactory8(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object, object> CreateFactory9(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object, object, object> CreateFactory10(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object, object, object, object> CreateFactory11(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object, object, object, object, object> CreateFactory12(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object> CreateFactory13(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> CreateFactory14(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> CreateFactory15(ConstructorInfo ci)
            {
                return null;
            }

            public Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> CreateFactory16(ConstructorInfo ci)
            {
                return null;
            }

            public Func<T> CreateFactory<T>()
            {
                return null;
            }

            public Func<TP1, T> CreateFactory<TP1, T>()
            {
                return null;
            }

            public Func<TP1, TP2, T> CreateFactory<TP1, TP2, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, T> CreateFactory<TP1, TP2, TP3, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, T> CreateFactory<TP1, TP2, TP3, TP4, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TP15, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TP15, T>()
            {
                return null;
            }

            public Func<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TP15, TP16, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TP15, TP16, T>()
            {
                return null;
            }

            public Func<object, object> CreateGetter(PropertyInfo pi)
            {
                return null;
            }

            public Func<object, object> CreateGetter(PropertyInfo pi, bool extension)
            {
                return null;
            }

            public Action<object, object> CreateSetter(PropertyInfo pi)
            {
                return null;
            }

            public Action<object, object> CreateSetter(PropertyInfo pi, bool extension)
            {
                return null;
            }

            public Func<T, TMember> CreateGetter<T, TMember>(PropertyInfo pi)
            {
                return null;
            }

            public Func<T, TMember> CreateGetter<T, TMember>(PropertyInfo pi, bool extension)
            {
                return null;
            }

            public Action<T, TMember> CreateSetter<T, TMember>(PropertyInfo pi)
            {
                return null;
            }

            public Action<T, TMember> CreateSetter<T, TMember>(PropertyInfo pi, bool extension)
            {
                return null;
            }

            public Type GetExtendedPropertyType(PropertyInfo pi)
            {
                return null;
            }
        }
    }
}
