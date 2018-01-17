namespace Smart.Navigation.Strategies
{
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

        // TODO parameter

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        // TODO basic

        // TODO parameter

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        // TODO exception

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

        public enum Pages
        {
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
