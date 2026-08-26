namespace Smart.Navigation;

using Smart.Mock;

public sealed class NavigatorDisposeTest
{
    [Fact]
    public static void NavigatorDispose()
    {
        // Arrange
        var component = new DisposableComponent();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .Configure(config => config.Add(component))
            .ToNavigator();

        // Act
        navigator.Dispose();

        // Assert
        Assert.True(component.IsDisposed);
    }

    public sealed class DisposableComponent : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
