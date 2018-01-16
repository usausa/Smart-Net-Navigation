namespace Smart.Navigation.Strategies
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class PopStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorPop()
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

            var page1 = (MockPage)navigator.CurrentPage;
            page1.Focused = "text1";

            navigator.Push(Pages.Page2);
            navigator.Pop();

            Assert.Equal(1, navigator.StackedCount);
            var page1B = (MockPage)navigator.CurrentPage;
            Assert.Same(page1, page1B);
            Assert.Equal(typeof(Page1), page1B.GetType());
            Assert.True(page1B.IsOpen);
            Assert.True(page1B.IsVisible);
            Assert.Equal("text1", page1B.Focused);

            Assert.Equal(Pages.Page2, context.Value.FromId);
            Assert.Equal(Pages.Page1, context.Value.ToId);
            Assert.True(context.Value.IsRestore());
        }

        [Fact]
        public static void TestNavigatorPopMultiple()
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
            navigator.Push(Pages.Page2);
            navigator.Push(Pages.Page3);
            navigator.Pop(2);

            Assert.Equal(1, navigator.StackedCount);
            var page1 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page1), page1.GetType());
            Assert.True(page1.IsOpen);
            Assert.True(page1.IsVisible);

            Assert.Equal(Pages.Page3, context.Value.FromId);
            Assert.Equal(Pages.Page1, context.Value.ToId);
            Assert.True(context.Value.IsRestore());
        }

        [Fact]
        public static void TestNavigatorPopWithParameter()
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
            navigator.Push(Pages.Page2);
            navigator.Pop(new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static void TestNavigatorPopMultipleWithParameter()
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
            navigator.Push(Pages.Page2);
            navigator.Push(Pages.Page3);
            navigator.Pop(2, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorPopFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
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
                .UseMockPageProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Pop(0));
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
