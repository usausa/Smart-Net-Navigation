namespace Smart.Navigation.Plugins;

using Smart.Mock;

public sealed class CommonParameterPluginTest
{
    // ------------------------------------------------------------
    // Helper plugin implementations
    // ------------------------------------------------------------

    private sealed class InjectingPlugin : PluginBase
    {
        private readonly Action<IMutableNavigationParameter> inject;

        public bool PrepareParameterCalled { get; private set; }

        public InjectingPlugin(Action<IMutableNavigationParameter> inject)
        {
            this.inject = inject;
        }

        public override void OnPrepareParameter(
            IPluginContext pluginContext,
            INavigationContext navigationContext,
            IMutableNavigationParameter parameter)
        {
            PrepareParameterCalled = true;
            inject(parameter);
        }
    }

    private sealed class PluginContextRelayPlugin : PluginBase
    {
        public override void OnPrepareParameter(
            IPluginContext pluginContext,
            INavigationContext navigationContext,
            IMutableNavigationParameter parameter)
        {
            // Save a value in PluginContext during OnPrepareParameter
            pluginContext.Save(typeof(string), "saved-value");
        }

        public override void OnNavigatingTo(
            IPluginContext pluginContext,
            INavigationContext navigationContext,
            object view,
            object? target)
        {
            // Read the saved value and inject it into parameter (for verification)
            var saved = pluginContext.Load<string>(typeof(string));
            ((IMutableNavigationParameter)navigationContext.Parameter).SetValue("relay", saved);
        }
    }

#pragma warning disable CA1812
    // ReSharper disable once UnusedType.Local
    private sealed class ToForm : MockForm
    {
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    // ReSharper disable once UnusedType.Local
    private sealed class ConfirmForm : MockForm, INavigationEventSupport
    {
        public string? InjectedValue { get; private set; }

        public void OnNavigatingFrom(INavigationContext context)
        {
        }

        public void OnNavigatingTo(INavigationContext context)
        {
            InjectedValue = context.Parameter.GetValueOrDefault<string>("session");
        }

        public void OnNavigatedTo(INavigationContext context)
        {
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    // ReSharper disable once UnusedType.Local
    private sealed class ConfirmCancelForm : MockForm, IConfirmRequest
    {
        public bool CanNavigate(INavigationContext context)
        {
            // Verify that OnPrepareParameter values are visible during Confirm
            return context.Parameter.GetValueOrDefault<string>("session") == "expected";
        }
    }
#pragma warning restore CA1812

    // ------------------------------------------------------------
    // Tests
    // ------------------------------------------------------------

    [Fact]
    public void NullParameterInjectedByPlugin()
    {
        // arrange
        var plugin = new InjectingPlugin(p => p.SetValue("session", "S1"));
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(plugin)
            .ToNavigator();

        INavigationParameter? capturedParameter = null;
        navigator.Navigated += (_, args) => capturedParameter = args.Context.Parameter;

        // act
        navigator.Forward(typeof(ToForm));

        // assert
        Assert.NotNull(capturedParameter);
        Assert.Equal("S1", capturedParameter.GetValue<string>("session"));
    }

    [Fact]
    public void CallerAndPluginParametersBothVisible()
    {
        // arrange
        var plugin = new InjectingPlugin(p => p.SetValue("b", 2));
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(plugin)
            .ToNavigator();

        INavigationParameter? capturedParameter = null;
        navigator.Navigated += (_, args) => capturedParameter = args.Context.Parameter;

        // act
        navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("a", 1));

        // assert
        Assert.NotNull(capturedParameter);
        Assert.Equal(1, capturedParameter.GetValue<int>("a"));
        Assert.Equal(2, capturedParameter.GetValue<int>("b"));
    }

    [Fact]
    public void PluginOverwritesCallerValue()
    {
        // arrange
        var plugin = new InjectingPlugin(p => p.SetValue("a", 2));
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(plugin)
            .ToNavigator();

        INavigationParameter? capturedParameter = null;
        navigator.Navigated += (_, args) => capturedParameter = args.Context.Parameter;

        // act
        navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("a", 1));

        // assert
        Assert.NotNull(capturedParameter);
        Assert.Equal(2, capturedParameter.GetValue<int>("a"));
    }

    [Fact]
    public void MultiplePluginsAllKeysVisible()
    {
        // arrange
        var pluginA = new InjectingPlugin(p => p.SetValue("x", "X"));
        var pluginB = new InjectingPlugin(p => p.SetValue("y", "Y"));
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(pluginA)
            .AddPlugin(pluginB)
            .ToNavigator();

        INavigationParameter? capturedParameter = null;
        navigator.Navigated += (_, args) => capturedParameter = args.Context.Parameter;

        // act
        navigator.Forward(typeof(ToForm));

        // assert
        Assert.NotNull(capturedParameter);
        Assert.Equal("X", capturedParameter.GetValue<string>("x"));
        Assert.Equal("Y", capturedParameter.GetValue<string>("y"));
    }

    [Fact]
    public void PluginContextSharedBetweenPrepareAndNavigatingTo()
    {
        // arrange
        var plugin = new PluginContextRelayPlugin();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(plugin)
            .ToNavigator();

        INavigationParameter? capturedParameter = null;
        navigator.Navigated += (_, args) => capturedParameter = args.Context.Parameter;

        // act
        navigator.Forward(typeof(ToForm));

        // assert
        Assert.NotNull(capturedParameter);
        Assert.Equal("saved-value", capturedParameter.GetValue<string>("relay"));
    }

    [Fact]
    public void PrepareParameterCalledEvenWhenConfirmCancels()
    {
        // arrange
        var plugin = new InjectingPlugin(p => p.SetValue("session", "S1"));
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(plugin)
            .ToNavigator();
        navigator.Confirm += static (_, args) => args.Cancel = true;

        // act
        var result = navigator.Forward(typeof(ToForm));

        // assert
        Assert.False(result);
        Assert.True(plugin.PrepareParameterCalled);
    }

    [Fact]
    public void PreparedParameterVisibleDuringConfirm()
    {
        // arrange
        var plugin = new InjectingPlugin(p => p.SetValue("session", "expected"));
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(plugin)
            .ToNavigator();

        bool? sessionVisibleInConfirm = null;
        navigator.Confirm += (_, args) =>
        {
            sessionVisibleInConfirm = args.Context.Parameter.GetValueOrDefault<string>("session") == "expected";
        };

        // act
        navigator.Forward(typeof(ToForm));

        // assert
        Assert.True(sessionVisibleInConfirm);
    }

    [Fact]
    public void DirectIPluginImplementationCompileAndWork()
    {
        // Verify that a class implementing IPlugin directly (without PluginBase) works
        // because OnPrepareParameter has a default interface method implementation.
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(new DirectIPluginImpl())
            .ToNavigator();

        // Should not throw
        var result = navigator.Forward(typeof(ToForm));
        Assert.True(result);
    }

    // Direct IPlugin implementation (no PluginBase) — verifies default interface method
    private sealed class DirectIPluginImpl : IPlugin
    {
        public void OnCreate(IPluginContext pluginContext, object view, object? target)
        {
        }

        public void OnClose(IPluginContext pluginContext, object view, object? target)
        {
        }

        public void OnNavigatingFrom(IPluginContext pluginContext, INavigationContext navigationContext, object? view, object? target)
        {
        }

        public void OnNavigatingTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object? target)
        {
        }

        public void OnNavigatedTo(IPluginContext pluginContext, INavigationContext navigationContext, object view, object? target)
        {
        }
    }
}
