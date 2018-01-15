namespace Smart.Navigation.Strategies
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class PushStrategyTest
    {
        [Fact]
        public static void TestNavigatorPush()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.Page3, typeof(Page3));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.Page1);

            Assert.Equal(1, navigator.StackedCount);
            var page1 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page1), page1.GetType());
            Assert.True(page1.IsOpen);

            navigator.Push(Pages.Page2);

            Assert.Equal(2, navigator.StackedCount);
            var page2 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page2), page2.GetType());
            Assert.True(page2.IsOpen);
            Assert.True(page1.IsOpen);
            Assert.False(page1.IsVisible);

            Assert.Equal(Pages.Page1, context.Value.FromId);
            Assert.Equal(Pages.Page2, context.Value.ToId);
            Assert.True(context.Value.IsStacked());

            navigator.Push(Pages.Page3);

            Assert.Equal(3, navigator.StackedCount);
            var page3 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page3), page3.GetType());
            Assert.True(page3.IsOpen);
            Assert.True(page2.IsOpen);
            Assert.False(page2.IsVisible);

            Assert.Equal(Pages.Page2, context.Value.FromId);
            Assert.Equal(Pages.Page3, context.Value.ToId);
            Assert.True(context.Value.IsStacked());
        }

        [Fact]
        public static void TestNavigatorPushFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Forward(Pages.Page1));
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
