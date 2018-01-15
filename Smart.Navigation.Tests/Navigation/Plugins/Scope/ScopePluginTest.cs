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

            navigator.Register(Pages.DataPage1, typeof(DataPage1));
            navigator.Register(Pages.DataPage2, typeof(DataPage2));
            navigator.Register(Pages.DataPage3, typeof(DataPage3));

            // test
            navigator.Forward(Pages.DataPage1);

            navigator.Forward(Pages.DataPage2);

            var page2 = (DataPage2)navigator.CurrentPage;
            Assert.NotNull(page2.Data);
            Assert.True(page2.Data.IsInitialized);
            Assert.False(page2.Data.IsDisposed);

            navigator.Forward(Pages.DataPage3);

            var page3 = (DataPage3)navigator.CurrentPage;
            Assert.Same(page3.Data, page2.Data);
            Assert.True(page3.Data.IsInitialized);
            Assert.False(page3.Data.IsDisposed);

            navigator.Forward(Pages.DataPage1);

            Assert.True(page3.Data.IsDisposed);
        }

        [Fact]
        public static void TestScopeByRequestType()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.ObjectPage1, typeof(ObjectPage1));
            navigator.Register(Pages.ObjectPage2, typeof(ObjectPage2));

            // test
            navigator.Forward(Pages.ObjectPage1);

            var page1 = (ObjectPage1)navigator.CurrentPage;
            Assert.NotNull(page1.Object);

            navigator.Forward(Pages.ObjectPage2);

            var page2 = (ObjectPage2)navigator.CurrentPage;
            Assert.Same(page2.Object, page1.Object);
        }

        public enum Pages
        {
            DataPage1,
            DataPage2,
            DataPage3,
            ObjectPage1,
            ObjectPage2
        }

        public class DataPage1 : MockPage
        {
        }

        public class DataPage2 : MockPage
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class DataPage3 : MockPage
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class ObjectPage1 : MockPage
        {
            [Scope(typeof(ScopeObject))]
            public IScopeObject Object { get; set; }
        }

        public class ObjectPage2 : MockPage
        {
            [Scope]
            public ScopeObject Object { get; set; }
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

        public interface IScopeObject
        {
            int Value { get; set; }
        }

        public class ScopeObject : IScopeObject
        {
            public int Value { get; set; }
        }
    }
}
