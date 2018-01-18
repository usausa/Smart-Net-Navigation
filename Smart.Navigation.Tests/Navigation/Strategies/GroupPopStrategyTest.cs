namespace Smart.Navigation.Strategies
{
    using System;
    using System.Threading.Tasks;

    using Smart.Mock;

    using Xunit;

    public class GroupPopStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorGropedPop()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.Page1);
            var page1 = (Page1)navigator.CurrentPage;

            navigator.GroupPush(Pages.PageA1);
            var pageA1 = (PageA1)navigator.CurrentPage;

            navigator.GroupPush(Pages.PageA2);
            var pageA2 = (PageA2)navigator.CurrentPage;

            navigator.GroupPop();

            Assert.Equal(1, navigator.StackedCount);
            Assert.Same(page1, navigator.CurrentPage);
            Assert.True(page1.IsVisible);
            Assert.False(pageA1.IsOpen);
            Assert.False(pageA2.IsOpen);

            Assert.Equal(Pages.PageA2, context.Value.FromId);
            Assert.Equal(Pages.Page1, context.Value.ToId);
            Assert.True(context.Value.Attribute.IsRestore());
        }

        [Fact]
        public static void TestNavigatorGropedPopLeaveLast()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.Page1);

            navigator.GroupPush(Pages.PageA1);
            var pageA1 = (PageA1)navigator.CurrentPage;

            navigator.GroupPush(Pages.PageA2);
            var pageA2 = (PageA2)navigator.CurrentPage;

            navigator.GroupPop(true);

            Assert.Equal(2, navigator.StackedCount);
            Assert.Same(pageA1, navigator.CurrentPage);
            Assert.True(pageA1.IsVisible);
            Assert.False(pageA2.IsOpen);

            Assert.Equal(Pages.PageA2, context.Value.FromId);
            Assert.Equal(Pages.PageA1, context.Value.ToId);
            Assert.True(context.Value.Attribute.IsRestore());
        }

        [Fact]
        public static void TestNavigatorGropedPopLeaveLastNoOperation()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));

            // test
            navigator.Forward(Pages.Page1);
            navigator.GroupPush(Pages.PageA1);

            Assert.False(navigator.GroupPop(true));
        }

        [Fact]
        public static void TestNavigatorGropedPopWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.Page1);
            navigator.GroupPush(Pages.PageA1);
            navigator.GroupPush(Pages.PageA2);
            navigator.GroupPop(new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static void TestNavigatorGropedPopLeaveLastWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.Page1);
            navigator.GroupPush(Pages.PageA1);
            navigator.GroupPush(Pages.PageA2);
            navigator.GroupPop(true, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        [Fact]
        public static async Task TestNavigatorGropedPopAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));

            // test
            await navigator.ForwardAsync(Pages.Page1);
            await navigator.GroupPushAsync(Pages.PageA1);
            await navigator.GroupPushAsync(Pages.PageA2);
            await navigator.GroupPopAsync();

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(Pages.Page1, navigator.CurrentPageId);
        }

        [Fact]
        public static async Task TestNavigatorGropedPopLeaveLastAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));

            // test
            await navigator.ForwardAsync(Pages.Page1);
            await navigator.GroupPushAsync(Pages.PageA1);
            await navigator.GroupPushAsync(Pages.PageA2);
            await navigator.GroupPopAsync(true);

            Assert.Equal(2, navigator.StackedCount);
            Assert.Equal(Pages.PageA1, navigator.CurrentPageId);
        }

        [Fact]
        public static async Task TestNavigatorGropedPopAsyncWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(Pages.Page1);
            await navigator.GroupPushAsync(Pages.PageA1);
            await navigator.GroupPushAsync(Pages.PageA2);
            await navigator.GroupPopAsync(new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static async Task TestNavigatorGropedPopAsyncLeaveLastWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(Pages.Page1);
            await navigator.GroupPushAsync(Pages.PageA1);
            await navigator.GroupPushAsync(Pages.PageA2);
            await navigator.GroupPopAsync(true, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorGropedPopFailed1()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        [Fact]
        public static void TestNavigatorGropedPopFailed2()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));

            // test
            navigator.Forward(Pages.Page1);
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        [Fact]
        public static void TestNavigatorGropedPopFailed3()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));

            // test
            navigator.GroupPush(Pages.PageA1);
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

        public enum Pages
        {
            Page1,
            Page2,
            PageA1,
            PageA2,
            PageB1,
            PageB2,
            PageC1,
            PageC2
        }

        public enum Groups
        {
            A,
            B,
            C
        }

        public class Page1 : MockPage
        {
        }

        public class Page2 : MockPage
        {
        }

        [Group(Groups.A)]
        public class PageA1 : MockPage
        {
        }

        [Group(Groups.A)]
        public class PageA2 : MockPage
        {
        }

        [Group(Groups.B)]
        public class PageB1 : MockPage
        {
        }

        [Group(Groups.B)]
        public class PageB2 : MockPage
        {
        }

        [Group(Groups.C)]
        public class PageC1 : MockPage
        {
        }

        [Group(Groups.C)]
        public class PageC2 : MockPage
        {
        }
    }
}
