namespace Smart.Navigation.Strategies
{
    using System;
    using System.Threading.Tasks;

    using Smart.Mock;

    using Xunit;

    public class PushStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorPush()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
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
            Assert.True(context.Value.Attribute.IsStacked());

            navigator.Push(Pages.Page3);

            Assert.Equal(3, navigator.StackedCount);
            var page3 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page3), page3.GetType());
            Assert.True(page3.IsOpen);
            Assert.True(page2.IsOpen);
            Assert.False(page2.IsVisible);

            Assert.Equal(Pages.Page2, context.Value.FromId);
            Assert.Equal(Pages.Page3, context.Value.ToId);
            Assert.True(context.Value.Attribute.IsStacked());
        }

        [Fact]
        public static void TestNavigatorPushWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.Page1);

            navigator.Push(Pages.Page2, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        [Fact]
        public static async Task TestNavigatorPushAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.Page3, typeof(Page3));

            // test
            await navigator.ForwardAsync(Pages.Page1);

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(Pages.Page1, navigator.CurrentPageId);

            await navigator.PushAsync(Pages.Page2);

            Assert.Equal(2, navigator.StackedCount);
            Assert.Equal(Pages.Page2, navigator.CurrentPageId);

            await navigator.PushAsync(Pages.Page3);

            Assert.Equal(3, navigator.StackedCount);
            Assert.Equal(Pages.Page3, navigator.CurrentPageId);
        }

        [Fact]
        public static async Task TestNavigatorPushAsyncWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(Pages.Page1);

            await navigator.PushAsync(Pages.Page2, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorPushFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Forward(Pages.Page1));
        }

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

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
