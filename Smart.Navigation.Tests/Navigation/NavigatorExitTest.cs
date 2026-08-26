namespace Smart.Navigation;

using Smart.Mock;

public sealed class NavigatorExitTest
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

    public sealed class Form1 : MockForm
    {
    }

    public sealed class Form2 : MockForm
    {
    }

    public sealed class Form3 : MockForm
    {
    }
}
