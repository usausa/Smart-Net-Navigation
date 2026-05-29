namespace Smart.Navigation.Plugins;

using Smart.Mock;

public sealed class ParameterPluginTest
{
#pragma warning disable CA1812
    // ReSharper disable once UnusedType.Local
    private sealed class ToForm : MockForm
    {
    }
#pragma warning restore CA1812

    // ------------------------------------------------------------
    // Tests
    // ------------------------------------------------------------

    private sealed class InjectingPlugin : PluginBase
    {
        private readonly Action<INavigationParameterPrepare> inject;

        public bool PrepareParameterCalled { get; private set; }

        public InjectingPlugin(Action<INavigationParameterPrepare> inject)
        {
            this.inject = inject;
        }

        public override void OnPrepareParameter(
            IPluginContext pluginContext,
            INavigationContext navigationContext,
            INavigationParameterPrepare parameter)
        {
            PrepareParameterCalled = true;
            inject(parameter);
        }
    }

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

    // ------------------------------------------------------------
    //
    // ------------------------------------------------------------

    private sealed class PluginContextRelayPlugin : PluginBase
    {
        public override void OnPrepareParameter(
            IPluginContext pluginContext,
            INavigationContext navigationContext,
            INavigationParameterPrepare parameter)
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
            ((INavigationParameterPrepare)navigationContext.Parameter).SetValue("relay", saved);
        }
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
}
