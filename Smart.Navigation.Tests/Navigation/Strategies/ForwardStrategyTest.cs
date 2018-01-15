namespace Smart.Navigation.Strategies
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class ForwardStrategyTest
    {
        [Fact]
        public static void TestNavigatorForward()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(1, typeof(Page1));
            navigator.Register(2, typeof(Page2));

            // test
            navigator.Forward(1);

            Assert.Equal(1, navigator.StackedCount);
            var page1 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page1), page1.GetType());
            Assert.True(page1.IsOpen);

            navigator.Forward(2);

            Assert.Equal(1, navigator.StackedCount);
            var page2 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page2), page2.GetType());
            Assert.True(page2.IsOpen);
            Assert.False(page1.IsOpen);
        }

        [Fact]
        public static void TestNavigatorForwardWithStacked()
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
            navigator.Forward(3);

            Assert.Equal(2, navigator.StackedCount);
            var page3 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page3), page3.GetType());
            Assert.True(page3.IsOpen);
        }

        [Fact]
        public static void TestNavigatorForwardFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Forward(1));
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
