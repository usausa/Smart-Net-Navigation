namespace Smart.Navigation.Mappers;

using Smart.Mock;

public sealed class DirectViewMapperTest
{
    [Fact]
    public static void DirectViewMapper()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseDirectViewMapper()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(Form1));

        // Assert
        Assert.Equal(typeof(Form1), navigator.CurrentView!.GetType());

        // Act
        navigator.Forward(typeof(Form2));

        // Assert
        Assert.Equal(typeof(Form2), navigator.CurrentView.GetType());
    }

    [Fact]
    public static void DirectViewMapperParameterFailed()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseDirectViewMapper()
            .ToNavigator();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(null!));
    }

    [Fact]
    public static void DirectViewMapperWithConstraintFailed()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseDirectViewMapper<string>()
            .ToNavigator();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(Form1)));
    }

    // ------------------------------------------------------------
    // Mock
    // ------------------------------------------------------------

    public sealed class Form1 : MockForm;

    public sealed class Form2 : MockForm;
}
