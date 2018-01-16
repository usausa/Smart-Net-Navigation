namespace Smart.Navigation
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class NavigatorDisposeTest
    {
        [Fact]
        public static void TestNavigatorDispose()
        {
            // prepare
            var component = new DisposableComponent();
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .Configure(config => config.Add(component))
                .ToNavigator();

            // test
            navigator.Dispose();

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
}
