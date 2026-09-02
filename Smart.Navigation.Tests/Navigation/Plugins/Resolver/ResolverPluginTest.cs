namespace Smart.Navigation.Plugins.Resolver;

using Smart.Mock;
using Smart.Resolver;

public sealed class ResolverPluginTest
{
    private const string ScopeName = nameof(ScopeName);

    [Fact]
    public static void Resolver()
    {
        // Arrange
        var config = new ResolverConfig();
        config.UseAutoBinding();
        config.UseInitializeProcessor();
        config.UsePageContextScope();
        config.Bind<ScopeObject>().ToSelf().InPageContextScope(ScopeName);

        var resolver = config.ToResolver();

        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseActivator(resolver)
            .AddResolverPlugin(resolver)
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Resolver1Form));

        navigator.Forward(typeof(Resolver2Form));

        // Assert
        var form2 = (Resolver2Form)navigator.CurrentView!;
        Assert.NotNull(form2.ScopeObject);
        Assert.True(form2.ScopeObject.IsInitialized);
        Assert.False(form2.ScopeObject.IsDisposed);

        // Act
        navigator.Forward(typeof(Resolver3Form));

        // Assert
        var form3 = (Resolver3Form)navigator.CurrentView!;
        Assert.NotNull(form3.ScopeObject);
        Assert.True(form3.ScopeObject.IsInitialized);
        Assert.False(form3.ScopeObject.IsDisposed);

        Assert.Equal(form2.ScopeObject, form3.ScopeObject);

        // Act
        navigator.Forward(typeof(Resolver1Form));

        // Assert
        Assert.True(form3.ScopeObject.IsDisposed);
    }

    [Fact]
    public static void ResolverTwice()
    {
        // Arrange
        var config = new ResolverConfig();
        config.UseAutoBinding();
        config.UseInitializeProcessor();
        config.UsePageContextScope();
        config.Bind<ScopeObject>().ToSelf().InPageContextScope(ScopeName);

        var resolver = config.ToResolver();

        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseActivator(resolver)
            .AddResolverPlugin(resolver)
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Resolver1Form));

        navigator.Forward(typeof(Resolver2Form));

        var form2A = (Resolver2Form)navigator.CurrentView!;

        navigator.Forward(typeof(Resolver1Form));

        // Assert
        Assert.NotNull(form2A.ScopeObject);
        Assert.True(form2A.ScopeObject.IsDisposed);

        // Act
        navigator.Forward(typeof(Resolver2Form));

        var form2B = (Resolver2Form)navigator.CurrentView!;

        // Assert
        Assert.NotNull(form2B.ScopeObject);
        Assert.False(form2B.ScopeObject.IsDisposed);

        Assert.NotSame(form2A.ScopeObject, form2B.ScopeObject);
    }

    [Fact]
    public static void ResolverSkipInTheMiddle()
    {
        // Arrange
        var config = new ResolverConfig();
        config.UseAutoBinding();
        config.UseInitializeProcessor();
        config.UsePageContextScope();
        config.Bind<ScopeObject>().ToSelf().InPageContextScope(ScopeName);

        var resolver = config.ToResolver();

        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseActivator(resolver)
            .AddResolverPlugin(resolver)
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Push1Form));

        navigator.Push(typeof(Push2Form));

        // Assert
        var form2 = (Push2Form)navigator.CurrentView!;
        Assert.NotNull(form2.ScopeObject);
        Assert.True(form2.ScopeObject.IsInitialized);
        Assert.False(form2.ScopeObject.IsDisposed);

        // Act
        navigator.Push(typeof(Push3Form));

        // Assert
        Assert.False(form2.ScopeObject.IsDisposed);

        // Act
        navigator.Push(typeof(Push4Form));

        // Assert
        var form4 = (Push4Form)navigator.CurrentView!;
        Assert.Equal(form4.ScopeObject, form2.ScopeObject);
        Assert.True(form4.ScopeObject.IsInitialized);
        Assert.False(form4.ScopeObject.IsDisposed);

        // Act
        navigator.Pop();

        // Assert
        Assert.False(form4.ScopeObject.IsDisposed);

        // Act
        navigator.Pop();

        // Assert
        Assert.False(form4.ScopeObject.IsDisposed);

        // Act
        navigator.Pop();

        // Assert
        Assert.True(form4.ScopeObject.IsDisposed);
    }

    public sealed class Resolver1Form : MockForm;

    [PageContext(ScopeName)]
    public sealed class Resolver2Form : MockForm
    {
        public ScopeObject ScopeObject { get; }

        public Resolver2Form(ScopeObject scope)
        {
            ScopeObject = scope;
        }
    }

    [PageContext(ScopeName)]
    public sealed class Resolver3Form : MockForm
    {
        public ScopeObject ScopeObject { get; }

        public Resolver3Form(ScopeObject scope)
        {
            ScopeObject = scope;
        }
    }

    public sealed class Push1Form : MockForm;

    [PageContext(ScopeName)]
    public sealed class Push2Form : MockForm
    {
        public ScopeObject ScopeObject { get; }

        public Push2Form(ScopeObject scope)
        {
            ScopeObject = scope;
        }
    }

    public sealed class Push3Form : MockForm;

    [PageContext(ScopeName)]
    public sealed class Push4Form : MockForm
    {
        public ScopeObject ScopeObject { get; }

        public Push4Form(ScopeObject scope)
        {
            ScopeObject = scope;
        }
    }

    public sealed class ScopeObject : IInitializable, IDisposable
    {
        public bool IsInitialized { get; private set; }

        public bool IsDisposed { get; private set; }

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
