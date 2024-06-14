namespace Smart.Navigation.Mappers;

using System.Reflection;

using Smart.Mock;
using Smart.Navigation.Attributes;

public sealed class IdViewMapperTest
{
    [Fact]
    public static void UseIdViewMapper()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseIdViewMapper(static r => r.AutoRegister([typeof(Form1), typeof(Form2)]))
            .ToNavigator();

        // test
        navigator.Forward(ViewId.Form1);

        Assert.Equal(typeof(Form1), navigator.CurrentView!.GetType());

        navigator.Forward(ViewId.Form2);

        Assert.Equal(typeof(Form2), navigator.CurrentView!.GetType());
    }

    [Fact]
    public static void UseIdViewMapperFindFailed()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseIdViewMapper(static _ => { })
            .ToNavigator();

        // test
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(ViewId.Form1));
    }

    [Fact]
    public static void UseIdViewMapperRegisterFailed()
    {
        Assert.Throws<TargetInvocationException>(() =>
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
    public sealed class Form1 : MockForm
    {
    }

    [View(ViewId.Form2)]
    public sealed class Form2 : MockForm
    {
    }
}
