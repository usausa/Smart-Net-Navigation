namespace Smart.Navigation;

using Smart.Mock;

public sealed class NavigatorAwareTest
{
    [Fact]
    public static void NavigatorAware()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(AwareForm));

        // Assert
        var awareForm = (AwareForm)navigator.CurrentView!;
        Assert.Same(navigator, awareForm.Navigator);
    }

    public sealed class AwareForm : MockForm, INavigatorAware
    {
        public INavigator Navigator { get; set; } = default!;
    }
}
