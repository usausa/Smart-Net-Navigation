namespace Smart.Navigation.Mappers;

using Smart.Mock;

public sealed class DirectViewMapperTest
{
    [Fact]
    public static void DirectViewMapper()
    {
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseDirectViewMapper()
            .ToNavigator();

        // test
        navigator.Forward(typeof(Form1));

        Assert.Equal(typeof(Form1), navigator.CurrentView!.GetType());

        navigator.Forward(typeof(Form2));

        Assert.Equal(typeof(Form2), navigator.CurrentView.GetType());
    }

    [Fact]
    public static void DirectViewMapperParameterFailed()
    {
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseDirectViewMapper()
            .ToNavigator();

        // test
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(null!));
    }

    [Fact]
    public static void DirectViewMapperWithConstraintFailed()
    {
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseDirectViewMapper<string>()
            .ToNavigator();

        // test
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(Form1)));
    }

    // ------------------------------------------------------------
    // Mock
    // ------------------------------------------------------------

    public sealed class Form1 : MockForm
    {
    }

    public sealed class Form2 : MockForm
    {
    }
}
