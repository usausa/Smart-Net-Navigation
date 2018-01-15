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

            navigator.Register(1, typeof(Page1));
            navigator.Register(2, typeof(Page2));

            // test
            navigator.Forward(1);
            navigator.Push(2);
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

            navigator.Register(1, typeof(Page1));
            navigator.Register(2, typeof(Page2));
            navigator.Register(3, typeof(Page3));

            // test
            navigator.Forward(1);
            navigator.Push(2);
            navigator.Push(3);
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
