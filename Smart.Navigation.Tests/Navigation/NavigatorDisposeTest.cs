namespace Smart.Navigation;

using Smart.Mock;

public sealed class NavigatorDisposeTest
{
#pragma warning disable CA2000
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
#pragma warning restore CA2000

    public sealed class DisposableComponent : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
