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

            Assert.True(components.TryGet<INavigationProvider>(out _));
        }

        [Fact]
        public static void ConfigUseProviderByInstance()
        {
            var config = new NavigatorConfig()
                .UseProvider(new MockFormNavigationProvider());

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.True(components.TryGet<INavigationProvider>(out _));
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

        // ------------------------------------------------------------
        // TypeConstraint
        // ------------------------------------------------------------

        [Fact]
        public static void ConfigUseTypeConstraintByInterface()
        {
            var config = new NavigatorConfig()
                .UseTypeConstraint<DummyTypeConstraint>();

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.True(components.TryGet<ITypeConstraint>(out _));
        }

        [Fact]
        public static void ConfigUseTypeConstraintByInstance()
        {
            var config = new NavigatorConfig()
                .UseTypeConstraint(new DummyTypeConstraint());

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.True(components.TryGet<ITypeConstraint>(out _));
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

            Assert.True(components.TryGet<IActivator>(out var activator));
            Assert.NotNull(activator!.Resolve(typeof(Model)));
        }

        [Fact]
        public static void ConfigUseActivatorByInstance()
        {
            var config = new NavigatorConfig()
                .UseActivator(new CallbackActivator(Activator.CreateInstance!));

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.True(components.TryGet<IActivator>(out var activator));
            Assert.NotNull(activator!.Resolve(typeof(Model)));
        }

        [Fact]
        public static void ConfigUseActivatorByCallback()
        {
            var config = new NavigatorConfig()
                .UseActivator(Activator.CreateInstance!);

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.True(components.TryGet<IActivator>(out var activator));
            Assert.NotNull(activator!.Resolve(typeof(Model)));
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

            Assert.True(components.TryGet<IConverter>(out var converter));
            Assert.Equal(1, converter!.Convert("1", typeof(int)));
        }

        [Fact]
        public static void ConfigUseConverterByInstance()
        {
            var config = new NavigatorConfig()
                .UseConverter(new StandardConverter());

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.True(components.TryGet<IConverter>(out var converter));
            Assert.Equal(1, converter!.Convert("1", typeof(int)));
        }

        [Fact]
        public static void ConfigUseConverterByObjectConverter()
        {
            var config = new NavigatorConfig()
                .UseConverter(ObjectConverter.Default);

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.True(components.TryGet<IConverter>(out var converter));
            Assert.Equal(1, converter!.Convert("1", typeof(int)));
        }

        [Fact]
        public static void ConfigUseConverterByCallback()
        {
            var config = new NavigatorConfig()
                .UseConverter(Convert.ChangeType);

            var components = ((INavigatorConfig)config).ResolveComponents();

            Assert.True(components.TryGet<IConverter>(out var converter));
            Assert.Equal(1, converter!.Convert("1", typeof(int)));
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

        // ------------------------------------------------------------
        // Utility
        // ------------------------------------------------------------

        public class Model
        {
        }

        public class DummyViewMapper : IViewMapper
        {
            public ViewDescriptor FindDescriptor(object id)
            {
                return new(id, typeof(object));
            }

            public void CurrentUpdated(object? id)
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

            public Func<int, Array> CreateArrayAllocator(Type type) =>
                throw new NotSupportedException();

            public Func<object> CreateFactory(Type type) =>
                throw new NotSupportedException();

            public Func<object?[]?, object> CreateFactory(Type type, Type[] argumentTypes) =>
                throw new NotSupportedException();

            public Func<object?[]?, object> CreateFactory(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object> CreateFactory0(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object> CreateFactory1(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object> CreateFactory2(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object> CreateFactory3(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object> CreateFactory4(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object> CreateFactory5(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object> CreateFactory6(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object> CreateFactory7(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory8(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory9(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory10(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory11(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory12(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory13(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory14(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory15(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object?, object> CreateFactory16(ConstructorInfo ci) =>
                throw new NotSupportedException();

            public Func<T> CreateFactory<T>() =>
                throw new NotSupportedException();

            public Func<TP1?, T> CreateFactory<TP1, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, T> CreateFactory<TP1, TP2, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, T> CreateFactory<TP1, TP2, TP3, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, T> CreateFactory<TP1, TP2, TP3, TP4, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, TP9?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, TP9?, TP10?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, TP9?, TP10?, TP11?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, TP9?, TP10?, TP11?, TP12?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, TP9?, TP10?, TP11?, TP12?, TP13?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, TP9?, TP10?, TP11?, TP12?, TP13?, TP14?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, TP9?, TP10?, TP11?, TP12?, TP13?, TP14?, TP15?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TP15, T>() =>
                throw new NotSupportedException();

            public Func<TP1?, TP2?, TP3?, TP4?, TP5?, TP6?, TP7?, TP8?, TP9?, TP10?, TP11?, TP12?, TP13?, TP14?, TP15?, TP16?, T> CreateFactory<TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TP15, TP16, T>() =>
                throw new NotSupportedException();

            public Func<object?, object?> CreateGetter(PropertyInfo pi) =>
                throw new NotSupportedException();

            public Func<object?, object?> CreateGetter(PropertyInfo pi, bool extension) =>
                throw new NotSupportedException();

            public Action<object?, object?> CreateSetter(PropertyInfo pi) =>
                throw new NotSupportedException();

            public Action<object?, object?> CreateSetter(PropertyInfo pi, bool extension) =>
                throw new NotSupportedException();

            public Func<T?, TMember?> CreateGetter<T, TMember>(PropertyInfo pi) =>
                throw new NotSupportedException();

            public Func<T?, TMember?> CreateGetter<T, TMember>(PropertyInfo pi, bool extension) =>
                throw new NotSupportedException();

            public Action<T?, TMember?> CreateSetter<T, TMember>(PropertyInfo pi) =>
                throw new NotSupportedException();

            public Action<T?, TMember?> CreateSetter<T, TMember>(PropertyInfo pi, bool extension) =>
                throw new NotSupportedException();

            public Type GetExtendedPropertyType(PropertyInfo pi) =>
                throw new NotSupportedException();
        }
    }
}
