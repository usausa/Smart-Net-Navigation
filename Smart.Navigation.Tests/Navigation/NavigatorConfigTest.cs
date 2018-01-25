﻿namespace Smart.Navigation
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Smart.Mock;
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;
    using Smart.Reflection;

    using Xunit;

    public static class NavigatorConfigTest
    {
        // ------------------------------------------------------------
        // Provider
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorConfigUseProviderByInterface()
        {
            var config = new NavigatorConfig()
                .UseProvider<MockFormNavigationProvider>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.NotNull(components.TryGet<INavigationProvider>());
        }

        [Fact]
        public static void TestNavigatorConfigUseProviderByInstance()
        {
            var config = new NavigatorConfig()
                .UseProvider(new MockFormNavigationProvider());

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.NotNull(components.TryGet<INavigationProvider>());
        }

        [Fact]
        public static void TestNavigatorConfigUseProviderByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseProvider(null));
        }

        // ------------------------------------------------------------
        // Factory
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorConfigUseFactoryByInterface()
        {
            var config = new NavigatorConfig()
                .UseFactory<StandardFactory>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var factory = components.TryGet<IFactory>();
            Assert.NotNull(factory);
            Assert.NotNull(factory.Create(typeof(Data)));
        }

        [Fact]
        public static void TestNavigatorConfigUseFactoryByInstance()
        {
            var config = new NavigatorConfig()
                .UseFactory(new CallbackFactory(Activator.CreateInstance));

            var components = ((INavigatorConfig)config).ResolveComponents();

            var factory = components.TryGet<IFactory>();
            Assert.NotNull(factory);
            Assert.NotNull(factory.Create(typeof(Data)));
        }

        [Fact]
        public static void TestNavigatorConfigUseFactoryByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseFactory((IFactory)null));
        }

        [Fact]
        public static void TestNavigatorConfigUseFactoryByCallback()
        {
            var config = new NavigatorConfig()
                .UseFactory(Activator.CreateInstance);

            var components = ((INavigatorConfig)config).ResolveComponents();

            var factory = components.TryGet<IFactory>();
            Assert.NotNull(factory);
            Assert.NotNull(factory.Create(typeof(Data)));
        }

        [Fact]
        public static void TestNavigatorConfigUseFactoryByCallbackFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseFactory((Func<Type, object>)null));
        }

        // ------------------------------------------------------------
        // Converter
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorConfigUseConverterByInterface()
        {
            var config = new NavigatorConfig()
                .UseConverter<StandardConverter>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var converter = components.TryGet<IConverter>();
            Assert.NotNull(converter);
            Assert.Equal(1, converter.Convert("1", typeof(int)));
        }

        [Fact]
        public static void TestNavigatorConfigUseConverterByInstance()
        {
            var config = new NavigatorConfig()
                .UseConverter(new StandardConverter());

            var components = ((INavigatorConfig)config).ResolveComponents();

            var converter = components.TryGet<IConverter>();
            Assert.NotNull(converter);
            Assert.Equal(1, converter.Convert("1", typeof(int)));
        }

        [Fact]
        public static void TestNavigatorConfigUseConverterByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseConverter((IConverter)null));
        }

        [Fact]
        public static void TestNavigatorConfigUseConverterByCallback()
        {
            var config = new NavigatorConfig()
                .UseConverter(Convert.ChangeType);

            var components = ((INavigatorConfig)config).ResolveComponents();

            var converter = components.TryGet<IConverter>();
            Assert.NotNull(converter);
            Assert.Equal(1, converter.Convert("1", typeof(int)));
        }

        [Fact]
        public static void TestNavigatorConfigUseConverterByCallbackFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseConverter((IConverter)null));
        }

        // ------------------------------------------------------------
        // Plugin
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorConfigAddPluginByInterface()
        {
            var config = new NavigatorConfig()
                .AddPlugin<DummyPlugin>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var plugin = components.GetAll<IPlugin>().FirstOrDefault(x => x.GetType() == typeof(DummyPlugin));
            Assert.NotNull(plugin);
        }

        [Fact]
        public static void TestNavigatorConfigAddPluginByInstance()
        {
            var config = new NavigatorConfig()
                .AddPlugin(new DummyPlugin());

            var components = ((INavigatorConfig)config).ResolveComponents();

            var plugin = components.GetAll<IPlugin>().FirstOrDefault(x => x.GetType() == typeof(DummyPlugin));
            Assert.NotNull(plugin);
        }

        [Fact]
        public static void TestNavigatorConfigAddPluginByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().AddPlugin(null));
        }

        // ------------------------------------------------------------
        // IDelegateFactory
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorConfigUseAccessorByInterface()
        {
            var config = new NavigatorConfig()
                .UseDelegateFactory<DummyDelegateFactory>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            var delegateFactory = components.Get<IDelegateFactory>();
            Assert.NotNull(delegateFactory);
        }

        [Fact]
        public static void TestNavigatorConfigUseAccessorByInstance()
        {
            var config = new NavigatorConfig()
                .UseDelegateFactory(new DummyDelegateFactory());

            var components = ((INavigatorConfig)config).ResolveComponents();

            var delegateFactory = components.Get<IDelegateFactory>();
            Assert.NotNull(delegateFactory);
        }

        [Fact]
        public static void TestNavigatorConfigUseAccessorByInstanceFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new NavigatorConfig().UseDelegateFactory(null));
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorConfigFailed()
        {
            var config = new NavigatorConfig();

            Assert.Throws<ArgumentNullException>(() => config.Configure(null));
        }

        // ------------------------------------------------------------
        // utilitity
        // ------------------------------------------------------------

        public class Data
        {
        }

        public class DummyPlugin : PluginBase
        {
        }

        public class DummyDelegateFactory : IDelegateFactory
        {
            public Func<int, Array> CreateArrayAllocator(Type type)
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

            public Type GetExtendedPropertyType(PropertyInfo pi)
            {
                return null;
            }
        }
    }
}
