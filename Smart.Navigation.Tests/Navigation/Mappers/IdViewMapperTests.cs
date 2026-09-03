namespace Smart.Navigation.Mappers;

using System.Reflection;

using Smart.Mock;
using Smart.Navigation.Attributes;

public sealed class IdViewMapperTests
{
    [Fact]
    public static void UseIdViewMapper()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseIdViewMapper(static r => r.AutoRegister([typeof(Form1), typeof(Form2)]))
            .ToNavigator();

        // Act
        navigator.Forward(ViewId.Form1);

        // Assert
        Assert.Equal(typeof(Form1), navigator.CurrentView!.GetType());

        // Act
        navigator.Forward(ViewId.Form2);

        // Assert
        Assert.Equal(typeof(Form2), navigator.CurrentView!.GetType());
    }

    [Fact]
    public static void UseIdViewMapperFindFailed()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseIdViewMapper(static _ => { })
            .ToNavigator();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(ViewId.Form1));
    }

    [Fact]
    public static void UseIdViewMapperRegisterFailed()
    {
        Assert.Throws<TargetInvocationException>(static () =>
            new NavigatorConfig().UseMockFormProvider().UseIdViewMapper(static r => r.Register(1, typeof(string))).ToNavigator());
    }

    // ------------------------------------------------------------
    // Mock
    // ------------------------------------------------------------

    public enum ViewId
    {
        Form1,
        Form2
    }

    [View(ViewId.Form1)]
    public sealed class Form1 : MockForm;

    [View(ViewId.Form2)]
    public sealed class Form2 : MockForm;
}
