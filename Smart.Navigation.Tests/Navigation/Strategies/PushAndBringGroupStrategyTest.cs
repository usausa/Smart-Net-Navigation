namespace Smart.Navigation.Strategies
{
    using System;
    using System.Threading.Tasks;

    using Smart.Mock;

    using Xunit;

    public class PushAndBringGroupStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorGropedPushNewGroup()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageB1, typeof(PageB1));
            navigator.Register(Pages.PageC1, typeof(PageC1));

            // test
            // A1 B1       new []
            // A1 B1 C1    D:B1 C:C1
            navigator.Forward(Pages.PageA1);

            navigator.PushAndBringGroup(Pages.PageB1);

            var pageB1 = (PageB1)navigator.CurrentPage;

            navigator.PushAndBringGroup(Pages.PageC1);

            Assert.Equal(3, navigator.StackedCount);
            Assert.False(pageB1.IsVisible);
        }

        [Fact]
        public static void TestNavigatorGropedPushNewAndBring()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));
            navigator.Register(Pages.PageB1, typeof(PageB1));

            // test
            // A1 B1       new [0]
            // B1 A1 A2    D:B1 C:A2
            navigator.Forward(Pages.PageA1);

            var pageA1 = (PageA1)navigator.CurrentPage;

            navigator.PushAndBringGroup(Pages.PageB1);

            var pageB1 = (PageB1)navigator.CurrentPage;

            navigator.PushAndBringGroup(Pages.PageA2);

            Assert.Equal(3, navigator.StackedCount);
            Assert.False(pageB1.IsVisible);

            navigator.Pop();

            Assert.True(pageA1.IsVisible);
            Assert.False(pageB1.IsVisible);

            navigator.Pop();

            Assert.False(pageA1.IsOpen);
            Assert.True(pageB1.IsVisible);
        }

        [Fact]
        public static void TestNavigatorGropedPushNewAndNotBring()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageB1, typeof(PageB1));
            navigator.Register(Pages.PageB2, typeof(PageB2));

            // test
            // A1 B1       new [1]
            // A1 B1 B2    D:B1 C:B2
            navigator.Forward(Pages.PageA1);

            var pageA1 = (PageA1)navigator.CurrentPage;

            navigator.PushAndBringGroup(Pages.PageB1);

            var pageB1 = (PageB1)navigator.CurrentPage;

            navigator.PushAndBringGroup(Pages.PageB2);

            Assert.Equal(3, navigator.StackedCount);
            Assert.False(pageB1.IsVisible);

            navigator.Pop();

            Assert.False(pageA1.IsVisible);
            Assert.True(pageB1.IsVisible);

            navigator.Pop();

            Assert.True(pageA1.IsVisible);
            Assert.False(pageB1.IsOpen);
        }

        [Fact]
        public static void TestNavigatorGropedPushExistAndBring()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageB1, typeof(PageB1));

            // test
            // A1 B1       exist [0]
            // B1 A1       D:B1 A:A1
            navigator.Forward(Pages.PageA1);

            var pageA1 = (PageA1)navigator.CurrentPage;

            navigator.PushAndBringGroup(Pages.PageB1);

            var pageB1 = (PageB1)navigator.CurrentPage;

            navigator.PushAndBringGroup(Pages.PageA1);

            Assert.Equal(2, navigator.StackedCount);
            Assert.True(pageA1.IsVisible);
            Assert.False(pageB1.IsVisible);

            navigator.Pop();

            Assert.False(pageA1.IsOpen);
            Assert.True(pageB1.IsVisible);
        }

        [Fact]
        public static void TestNavigatorGropedPushExistAndNotBring()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageB1, typeof(PageB1));

            // test
            // A1 B1       exist [1]
            // A1 B1       -
            navigator.Forward(Pages.PageA1);

            var pageA1 = (PageA1)navigator.CurrentPage;

            navigator.PushAndBringGroup(Pages.PageB1);

            var pageB1 = (PageB1)navigator.CurrentPage;

            Assert.False(navigator.PushAndBringGroup(Pages.PageB1));

            Assert.Equal(2, navigator.StackedCount);
            Assert.False(pageA1.IsVisible);
            Assert.True(pageB1.IsVisible);

            navigator.Pop();

            Assert.True(pageA1.IsVisible);
            Assert.False(pageB1.IsOpen);
        }

        [Fact]
        public static void TestNavigatorGropedPushUseCase()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageA2, typeof(PageA2));
            navigator.Register(Pages.PageB1, typeof(PageB1));
            navigator.Register(Pages.PageB2, typeof(PageB2));

            // test
            navigator.Forward(Pages.Page1);

            // group A
            // 1:A1
            navigator.PushAndBringGroup(Pages.PageA1);
            var pageA1 = (PageA1)navigator.CurrentPage;

            // 1:A1:A2
            navigator.PushAndBringGroup(Pages.PageA2);
            var pageA2 = (PageA2)navigator.CurrentPage;

            // group B
            // 1:A1:A2:B1
            navigator.PushAndBringGroup(Pages.PageB1);
            var pageB1 = (PageB1)navigator.CurrentPage;

            // 1:A1:A2:B1:B2
            navigator.PushAndBringGroup(Pages.PageB2);
            var pageB2 = (PageB2)navigator.CurrentPage;

            // group A
            // 1:B1:B2:A1:A2
            navigator.PushAndBringGroup(Pages.PageA1);

            Assert.True(pageA2.IsVisible);
            Assert.False(pageA1.IsVisible);
            Assert.False(pageB2.IsVisible);
            Assert.False(pageB1.IsVisible);

            // 1:B1:B2:A1
            navigator.Pop();

            Assert.True(pageA1.IsVisible);
            Assert.False(pageB2.IsVisible);
            Assert.False(pageB1.IsVisible);

            // group B
            // 1:A1:B1:B2
            navigator.PushAndBringGroup(Pages.PageB1);

            Assert.True(pageB2.IsVisible);
            Assert.False(pageB1.IsVisible);
            Assert.False(pageA1.IsVisible);

            // 1:A1:B1
            navigator.Pop();

            Assert.True(pageB1.IsVisible);
            Assert.False(pageA1.IsVisible);
        }

        [Fact]
        public static void TestNavigatorGropedPushWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageB1, typeof(PageB1));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.PageA1);

            navigator.PushAndBringGroup(Pages.PageB1, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        [Fact]
        public static async Task TestNavigatorGropedPushAsyncExistAndNotBring()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageB1, typeof(PageB1));

            // test
            await navigator.ForwardAsync(Pages.PageA1);

            var pageA1 = (PageA1)navigator.CurrentPage;

            await navigator.PushAndBringGroupAsync(Pages.PageB1);

            var pageB1 = (PageB1)navigator.CurrentPage;

            Assert.False(await navigator.PushAndBringGroupAsync(Pages.PageB1));

            Assert.Equal(2, navigator.StackedCount);
            Assert.False(pageA1.IsVisible);
            Assert.True(pageB1.IsVisible);

            await navigator.PopAsync();

            Assert.True(pageA1.IsVisible);
            Assert.False(pageB1.IsOpen);
        }

        [Fact]
        public static async Task TestNavigatorGropedPushAsyncWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.PageA1, typeof(PageA1));
            navigator.Register(Pages.PageB1, typeof(PageB1));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(Pages.PageA1);

            await navigator.PushAndBringGroupAsync(Pages.PageB1, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorGropedPushFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));

            // test
            navigator.Forward(Pages.Page1);
            Assert.Throws<InvalidOperationException>(() => navigator.PushAndBringGroup(Pages.Page2));
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
