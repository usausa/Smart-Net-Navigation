namespace Smart.Navigation.Plugins.Scope;

using Smart.Mock;

#pragma warning disable CA1720
public sealed class ScopePluginTest
{
    [Fact]
    public static void Scope()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Data1Form));

        navigator.Forward(typeof(Data2Form));

        // Assert
        var form2 = (Data2Form)navigator.CurrentView!;
        Assert.NotNull(form2.Data);
        Assert.True(form2.Data.IsInitialized);
        Assert.False(form2.Data.IsDisposed);

        // Act
        navigator.Forward(typeof(Data3Form));

        // Assert
        var form3 = (Data3Form)navigator.CurrentView!;
        Assert.Equal(form3.Data, form2.Data);
        Assert.True(form3.Data.IsInitialized);
        Assert.False(form3.Data.IsDisposed);

        // Act
        navigator.Forward(typeof(Data1Form));

        // Assert: the scope end is notified before the resource cleanup
        Assert.True(form3.Data.IsTerminated);
        Assert.True(form3.Data.IsDisposed);
        Assert.True(form3.Data.TerminatedBeforeDispose);
    }

    [Fact]
    public static void ScopeByRequestType()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Object1Form));

        // Assert
        var form1 = (Object1Form)navigator.CurrentView!;
        Assert.NotNull(form1.Object);

        // Act
        navigator.Forward(typeof(Object2Form));

        // Assert
        var form2 = (Object2Form)navigator.CurrentView!;
        Assert.Equal(form2.Object, form1.Object);

        // Act
        navigator.Forward(typeof(Object3Form));
    }

    [Fact]
    public static void ScopeByGenericRequestType()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(GenericObject1Form));

        // Assert
        var form1 = (GenericObject1Form)navigator.CurrentView!;
        Assert.NotNull(form1.Object);

        // Act
        navigator.Forward(typeof(Object2Form));

        // Assert
        var form2 = (Object2Form)navigator.CurrentView!;
        Assert.Equal(form2.Object, form1.Object);
    }

    [Fact]
    public static void ScopeRequestTypeMustBeConcrete()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(InterfaceScopeForm)));
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(AbstractScopeForm)));
    }

    [Fact]
    public static void ScopeObjectUnresolvedThrows()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseActivator(static type => type == typeof(ScopeObject) ? null : Activator.CreateInstance(type))
            .ToNavigator();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(Object2Form)));
    }

    [Fact]
    public static void ScopeSkipInTheMiddle()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Push1Form));

        navigator.Push(typeof(Push2Form));

        // Assert
        var form2 = (Push2Form)navigator.CurrentView!;
        Assert.NotNull(form2.Data);
        Assert.True(form2.Data.IsInitialized);
        Assert.False(form2.Data.IsDisposed);

        // Act
        navigator.Push(typeof(Push3Form));

        // Assert
        Assert.False(form2.Data.IsDisposed);

        // Act
        navigator.Push(typeof(Push4Form));

        // Assert
        var form4 = (Push4Form)navigator.CurrentView!;
        Assert.Equal(form4.Data, form2.Data);
        Assert.True(form4.Data.IsInitialized);
        Assert.False(form4.Data.IsDisposed);

        // Act
        navigator.Pop();

        // Assert
        Assert.False(form4.Data.IsDisposed);

        // Act
        navigator.Pop();

        // Assert
        Assert.False(form4.Data.IsDisposed);

        // Act
        navigator.Pop();

        // Assert
        Assert.True(form4.Data.IsDisposed);
    }

    [Fact]
    public static void ScopeNamed()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Named1Form));

        // Assert
        var form1 = (Named1Form)navigator.CurrentView!;
        Assert.NotNull(form1.ExportData);

        // Act
        navigator.Forward(typeof(Named2Form));

        // Assert
        var form2 = (Named2Form)navigator.CurrentView!;
        Assert.Equal(form2.ImportData, form1.ExportData);
    }

    [Fact]
    public static void ScopeNameSharedByDifferentTypes()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(SharedDataForm));

        // Assert
        var formData = (SharedDataForm)navigator.CurrentView!;
        Assert.NotNull(formData.Shared);
        Assert.False(formData.Shared.IsDisposed);

        // Act: the same property name with a different request type
        navigator.Forward(typeof(SharedObjectForm));

        // Assert: the entry is keyed by name and type, so the types are not mixed
        var formObject = (SharedObjectForm)navigator.CurrentView!;
        Assert.NotNull(formObject.Shared);

        // Assert: the entries are independent, so the first one ends with its own screen
        Assert.True(formData.Shared.IsDisposed);
    }

    public sealed class Data1Form : MockForm;

    public sealed class SharedDataForm : MockForm
    {
        [Scope]
        public ScopeData Shared { get; set; } = default!;
    }

    public sealed class SharedObjectForm : MockForm
    {
        [Scope]
        public ScopeObject Shared { get; set; } = default!;
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

    public sealed class Object3Form : MockForm;

    public sealed class Push1Form : MockForm;

    public sealed class Push2Form : MockForm
    {
        [Scope]
        public ScopeData Data { get; set; } = default!;
    }

    public sealed class Push3Form : MockForm;

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

    public sealed class ScopeData : IScopeLifecycle, IDisposable
    {
        public bool IsInitialized { get; private set; }

        public bool IsTerminated { get; private set; }

        public bool IsDisposed { get; private set; }

        public bool TerminatedBeforeDispose { get; private set; }

        public void OnScopeInitialize()
        {
            IsInitialized = true;
        }

        public void OnScopeTerminate()
        {
            IsTerminated = true;
            TerminatedBeforeDispose = !IsDisposed;
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

    public abstract class AbstractScopeObject;

    public sealed class AbstractScopeForm : MockForm
    {
        [Scope]
        public AbstractScopeObject Object { get; set; } = default!;
    }
}
#pragma warning restore CA1720
