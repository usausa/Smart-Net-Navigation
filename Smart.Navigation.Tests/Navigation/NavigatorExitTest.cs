namespace Smart.Navigation
{
    using Smart.Mock;

    using Xunit;

    public class NavigatorExitTest
    {
        [Fact]
        public static void TestNavigatorExit()
        {
            // prepare
            var called = new Holder<bool>();
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();
            navigator.Exited += (sender, args) => called.Value = true;

            navigator.Register(Pages.Page1, typeof(Page1));

            // test
            navigator.Forward(Pages.Page1);

            var page1 = (Page1)navigator.CurrentPage;

            navigator.Exit();

            Assert.True(called.Value);
            Assert.False(page1.IsOpen);
        }

        [Fact]
        public static void TestNavigatorExitStacked()
        {
            // prepare
            var called = new Holder<bool>();
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();
            navigator.Exited += (sender, args) => called.Value = true;

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.Page3, typeof(Page3));

            // test
            navigator.Forward(Pages.Page1);

            var page1 = (Page1)navigator.CurrentPage;

            navigator.Push(Pages.Page2);

            var page2 = (Page2)navigator.CurrentPage;

            navigator.Push(Pages.Page3);

            var page3 = (Page3)navigator.CurrentPage;

            navigator.Exit();

            Assert.True(called.Value);
            Assert.False(page1.IsOpen);
            Assert.False(page2.IsOpen);
            Assert.False(page3.IsOpen);
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
