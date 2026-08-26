namespace Smart.Navigation.Plugins.Scope;

using Smart.Mock;

public sealed class ScopePluginTest
{
    [Fact]
    public static void Scope()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        navigator.Forward(typeof(Data1Form));

        navigator.Forward(typeof(Data2Form));

        var form2 = (Data2Form)navigator.CurrentView!;
        Assert.NotNull(form2.Data);
        Assert.True(form2.Data.IsInitialized);
        Assert.False(form2.Data.IsDisposed);

        navigator.Forward(typeof(Data3Form));

        var form3 = (Data3Form)navigator.CurrentView!;
        Assert.Equal(form3.Data, form2.Data);
        Assert.True(form3.Data.IsInitialized);
        Assert.False(form3.Data.IsDisposed);

        navigator.Forward(typeof(Data1Form));

        Assert.True(form3.Data.IsDisposed);
    }

    [Fact]
    public static void ScopeByRequestType()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        navigator.Forward(typeof(Object1Form));

        var form1 = (Object1Form)navigator.CurrentView!;
        Assert.NotNull(form1.Object);

        navigator.Forward(typeof(Object2Form));

        var form2 = (Object2Form)navigator.CurrentView!;
        Assert.Equal(form2.Object, form1.Object);

        navigator.Forward(typeof(Object3Form));
    }

    [Fact]
    public static void ScopeByGenericRequestType()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        navigator.Forward(typeof(GenericObject1Form));

        var form1 = (GenericObject1Form)navigator.CurrentView!;
        Assert.NotNull(form1.Object);

        navigator.Forward(typeof(Object2Form));

        var form2 = (Object2Form)navigator.CurrentView!;
        Assert.Equal(form2.Object, form1.Object);
    }

    [Fact]
    public static void ScopeRequestTypeMustBeConcrete()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(InterfaceScopeForm)));
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(AbstractScopeForm)));
    }

    [Fact]
    public static void ScopeObjectUnresolvedThrows()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseServiceProvider(static type => type == typeof(ScopeObject) ? null : Activator.CreateInstance(type))
            .ToNavigator();

        // test
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(Object2Form)));
    }

    [Fact]
    public static void ScopeSkipInTheMiddle()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        navigator.Forward(typeof(Push1Form));

        navigator.Push(typeof(Push2Form));

        var form2 = (Push2Form)navigator.CurrentView!;
        Assert.NotNull(form2.Data);
        Assert.True(form2.Data.IsInitialized);
        Assert.False(form2.Data.IsDisposed);

        navigator.Push(typeof(Push3Form));

        Assert.False(form2.Data.IsDisposed);

        navigator.Push(typeof(Push4Form));

        var form4 = (Push4Form)navigator.CurrentView!;
        Assert.Equal(form4.Data, form2.Data);
        Assert.True(form4.Data.IsInitialized);
        Assert.False(form4.Data.IsDisposed);

        navigator.Pop();

        Assert.False(form4.Data.IsDisposed);

        navigator.Pop();

        Assert.False(form4.Data.IsDisposed);

        navigator.Pop();

        Assert.True(form4.Data.IsDisposed);
    }

    [Fact]
    public static void ScopeNamed()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        navigator.Forward(typeof(Named1Form));

        var form1 = (Named1Form)navigator.CurrentView!;
        Assert.NotNull(form1.ExportData);

        navigator.Forward(typeof(Named2Form));

        var form2 = (Named2Form)navigator.CurrentView!;
        Assert.Equal(form2.ImportData, form1.ExportData);
    }

    public sealed class Data1Form : MockForm
    {
    }

    public sealed class Data2Form : MockForm
    {
        [Scope]
        public ScopeData Data { get; set; } = default!;
    }

    public sealed class Data3Form : MockForm
    {
        [Scope]
        public ScopeData Data { get; set; } = default!;
    }

    public sealed class Object1Form : MockForm
    {
        [Scope(typeof(ScopeObject))]
        public IScopeObject Object { get; set; } = default!;
    }

    public sealed class Object2Form : MockForm
    {
        [Scope]
        public ScopeObject Object { get; set; } = default!;
    }

    public sealed class Object3Form : MockForm
    {
    }

    public sealed class Push1Form : MockForm
    {
    }

    public sealed class Push2Form : MockForm
    {
        [Scope]
        public ScopeData Data { get; set; } = default!;
    }

    public sealed class Push3Form : MockForm
    {
    }

    public sealed class Push4Form : MockForm
    {
        [Scope]
        public ScopeData Data { get; set; } = default!;
    }

    public sealed class Named1Form : MockForm
    {
        [Scope("Data")]
        public ScopeData ExportData { get; set; } = default!;
    }

    public sealed class Named2Form : MockForm
    {
        [Scope("Data")]
        public ScopeData ImportData { get; set; } = default!;
    }

    public sealed class ScopeData : IInitializable, IDisposable
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

    public interface IScopeObject
    {
        int Value { get; set; }
    }

    public sealed class ScopeObject : IScopeObject
    {
        public int Value { get; set; }
    }

    public sealed class GenericObject1Form : MockForm
    {
        [Scope<ScopeObject>]
        public IScopeObject Object { get; set; } = default!;
    }

    public sealed class InterfaceScopeForm : MockForm
    {
        [Scope]
        public IScopeObject Object { get; set; } = default!;
    }

    public abstract class AbstractScopeObject
    {
    }

    public sealed class AbstractScopeForm : MockForm
    {
        [Scope]
        public AbstractScopeObject Object { get; set; } = default!;
    }
}
