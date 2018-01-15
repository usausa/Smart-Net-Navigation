namespace Smart.Navigation.Plugins.Scope
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class ScopePluginTest
    {
        [Fact]
        public static void TestScope()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.Page3, typeof(Page3));

            // test
            navigator.Forward(Pages.Page1);

            navigator.Forward(Pages.Page2);

            var page2 = (Page2)navigator.CurrentPage;
            Assert.NotNull(page2.Data);
            Assert.True(page2.Data.IsInitialized);
            Assert.False(page2.Data.IsDisposed);

            navigator.Forward(Pages.Page3);

            var page3 = (Page3)navigator.CurrentPage;
            Assert.Same(page3.Data, page2.Data);
            Assert.True(page3.Data.IsInitialized);
            Assert.False(page3.Data.IsDisposed);

            navigator.Forward(Pages.Page1);

            Assert.True(page3.Data.IsDisposed);
        }

        public enum Pages
        {
            Page1,
            Page2,
            Page3
        }

        public class Page1 : MockPage
        {
        }

        public class Page2 : MockPage
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class Page3 : MockPage
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public sealed class ScopeData : IInitializable, IDisposable
        {
            public bool IsInitialized { get; private set; }

            public bool IsDisposed { get; private set; }

            public void Initialize()
            {
                IsInitialized = true;
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }
    }
}
