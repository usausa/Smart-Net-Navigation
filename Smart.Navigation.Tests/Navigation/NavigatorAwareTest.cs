namespace Smart.Navigation
{
    using Smart.Mock;

    using Xunit;

    public class NavigatorAwareTest
    {
        [Fact]
        public static void TestNavigatorAware()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.AwarePage, typeof(AwarePage));

            // test
            navigator.Forward(Pages.AwarePage);

            var awarePage = (AwarePage)navigator.CurrentPage;
            Assert.Same(navigator, awarePage.Navigator);
        }

        public enum Pages
        {
            AwarePage
        }

        public class AwarePage : MockPage, INavigatorAware
        {
            public INavigator Navigator { get; set; }
        }
    }
}
