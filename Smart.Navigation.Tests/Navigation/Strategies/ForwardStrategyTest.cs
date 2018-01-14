namespace Smart.Navigation.Strategies
{
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

            var page1 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page1), page1.GetType());
            Assert.True(page1.IsOpen);

            navigator.Forward(2);

            var page2 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page2), page2.GetType());
            Assert.True(page2.IsOpen);
            Assert.False(page1.IsOpen);
        }

        public class Page1 : MockPage
        {
        }

        public class Page2 : MockPage
        {
        }
    }
}
