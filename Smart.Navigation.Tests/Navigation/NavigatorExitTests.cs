namespace Smart.Navigation;

using Smart.Mock;
using Smart.Navigation.Plugins;

public sealed class NavigatorExitTests
{
    [Fact]
    public static void Exit()
    {
        // Arrange
        var called = new Holder<bool>();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();
        navigator.Exited += (_, _) => called.Value = true;

        // Act
        navigator.Forward(typeof(Form1));

        var form1 = (Form1)navigator.CurrentView!;

        navigator.Exit();

        // Assert
        Assert.True(called.Value);
        Assert.False(form1.IsOpen);
    }

    [Fact]
    public static void ExitStacked()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Form1));

        var form1 = (Form1)navigator.CurrentView!;

        navigator.Push(typeof(Form2));

        var form2 = (Form2)navigator.CurrentView!;

        navigator.Push(typeof(Form3));

        var form3 = (Form3)navigator.CurrentView!;

        navigator.Exit();

        // Assert
        Assert.False(form1.IsOpen);
        Assert.False(form2.IsOpen);
        Assert.False(form3.IsOpen);
    }

    [Fact]
    public static void ExitNotifiesPluginsOfClose()
    {
        // Arrange
        var plugin = new ClosePlugin();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .AddPlugin(plugin)
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.Push(typeof(Form3));

        navigator.Exit();

        // Assert: closed from the top of the stack
        Assert.Equal([typeof(Form3), typeof(Form2), typeof(Form1)], plugin.Closed);
    }

    private sealed class ClosePlugin : PluginBase
    {
        public List<Type> Closed { get; } = [];

        public override void OnClose(IPluginContext pluginContext, object view, object? target)
        {
            Closed.Add(view.GetType());
        }
    }

    public sealed class Form1 : MockForm;

    public sealed class Form2 : MockForm;

    public sealed class Form3 : MockForm;
}
