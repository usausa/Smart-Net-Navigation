namespace Smart.Navigation.Strategies
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class PopStrategyTest
    {
        [Fact]
        public static void TestNavigatorPop()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            // test
            navigator.Forward(Pages.Page1);
            navigator.Push(Pages.Page2);
            navigator.Pop();

            Assert.Equal(1, navigator.StackedCount);
            var page1 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page1), page1.GetType());
            Assert.True(page1.IsOpen);
            Assert.True(page1.IsVisible);
        }

        [Fact]
        public static void TestNavigatorPopMultiple()
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
            navigator.Push(Pages.Page2);
            navigator.Push(Pages.Page3);
            navigator.Pop(2);

            Assert.Equal(1, navigator.StackedCount);
            var page1 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page1), page1.GetType());
            Assert.True(page1.IsOpen);
            Assert.True(page1.IsVisible);
        }

        [Fact]
        public static void TestNavigatorPopFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(1, typeof(Page1));
            navigator.Register(2, typeof(Page2));

            // test
            navigator.Forward(1);
            navigator.Push(2);
            Assert.Throws<InvalidOperationException>(() => navigator.Pop(2));
        }

        [Fact]
        public static void TestNavigatorPopFailed2()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Pop(0));
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
        }

        public class Page3 : MockPage
        {
        }
    }
}
