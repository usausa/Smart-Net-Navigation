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
            navigator.Register(Pages.ObjectPage3, typeof(ObjectPage3));

            // test
            navigator.Forward(Pages.ObjectPage1);

            var page1 = (ObjectPage1)navigator.CurrentPage;
            Assert.NotNull(page1.Object);

            navigator.Forward(Pages.ObjectPage2);

            var page2 = (ObjectPage2)navigator.CurrentPage;
            Assert.Same(page2.Object, page1.Object);

            navigator.Forward(Pages.ObjectPage3);
        }

        [Fact]
        public static void TestScopeSkipInTheMiddle()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.PushPage1, typeof(PushPage1));
            navigator.Register(Pages.PushPage2, typeof(PushPage2));
            navigator.Register(Pages.PushPage3, typeof(PushPage3));
            navigator.Register(Pages.PushPage4, typeof(PushPage4));

            // test
            navigator.Forward(Pages.PushPage1);

            navigator.Push(Pages.PushPage2);

            var page2 = (PushPage2)navigator.CurrentPage;
            Assert.NotNull(page2.Data);
            Assert.True(page2.Data.IsInitialized);
            Assert.False(page2.Data.IsDisposed);

            navigator.Push(Pages.PushPage3);

            Assert.False(page2.Data.IsDisposed);

            navigator.Push(Pages.PushPage4);

            var page4 = (PushPage4)navigator.CurrentPage;
            Assert.Same(page4.Data, page2.Data);
            Assert.True(page4.Data.IsInitialized);
            Assert.False(page4.Data.IsDisposed);

            navigator.Pop();

            Assert.False(page4.Data.IsDisposed);

            navigator.Pop();

            Assert.False(page4.Data.IsDisposed);

            navigator.Pop();

            Assert.True(page4.Data.IsDisposed);
        }

        [Fact]
        public static void TestScopeNamed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.NamedPage1, typeof(NamedPage1));
            navigator.Register(Pages.PushPage2, typeof(PushPage2));

            // test
            navigator.Forward(Pages.NamedPage1);

            var page1 = (NamedPage1)navigator.CurrentPage;
            Assert.NotNull(page1.ExportData);

            navigator.Forward(Pages.NamedPage2);

            var page2 = (NamedPage2)navigator.CurrentPage;
            Assert.Same(page2.ImporttData, page1.ExportData);
        }

        public enum Pages
        {
            DataPage1,
            DataPage2,
            DataPage3,
            ObjectPage1,
            ObjectPage2,
            ObjectPage3,
            PushPage1,
            PushPage2,
            PushPage3,
            PushPage4,
            NamedPage1,
            NamedPage2
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

        public class ObjectPage3 : MockPage
        {
        }

        public class PushPage1 : MockPage
        {
        }

        public class PushPage2 : MockPage
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class PushPage3 : MockPage
        {
        }

        public class PushPage4 : MockPage
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class NamedPage1 : MockPage
        {
            [Scope("Data")]
            public ScopeData ExportData { get; set; }
        }

        public class NamedPage2 : MockPage
        {
            [Scope("Data")]
            public ScopeData ImporttData { get; set; }
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
