namespace Smart.Navigation.Strategies
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class PopAndForwardStrategyTest
    {
        [Fact]
        public static void TestNavigatorPopAndForward()
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
            navigator.Push(Pages.Page2);
            navigator.PopAndForward(Pages.Page3);

            Assert.Equal(2, navigator.StackedCount);
            var page3 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page3), page3.GetType());
            Assert.True(page3.IsOpen);
            Assert.True(page3.IsVisible);

            Assert.Equal(Pages.Page2, context.Value.FromId);
            Assert.Equal(Pages.Page3, context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
        }

        [Fact]
        public static void TestNavigatorPopAndForwardMultiple()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.Page3, typeof(Page3));
            navigator.Register(Pages.Page4, typeof(Page4));

            var context = new Holder<INavigationContext>();
            navigator.NavigatedTo += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(Pages.Page1);
            navigator.Push(Pages.Page2);
            navigator.Push(Pages.Page3);
            navigator.PopAndForward(Pages.Page4, 2);

            Assert.Equal(2, navigator.StackedCount);
            var page4 = (MockPage)navigator.CurrentPage;
            Assert.Equal(typeof(Page4), page4.GetType());
            Assert.True(page4.IsOpen);
            Assert.True(page4.IsVisible);

            Assert.Equal(Pages.Page3, context.Value.FromId);
            Assert.Equal(Pages.Page4, context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
        }

        [Fact]
        public static void TestNavigatorPopAndForwardFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            // test
            navigator.Forward(Pages.Page1);
            navigator.Push(Pages.Page2);
            Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(Pages.Page3));
        }

        [Fact]
        public static void TestNavigatorPopAndForwardFailed2()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.Page3, typeof(Page3));

            // test
            navigator.Forward(Pages.Page1);
            navigator.Push(Pages.Page2);
            Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(Pages.Page3, 3));
        }

        [Fact]
        public static void TestNavigatorPopAndForwardFailed3()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));
            navigator.Register(Pages.Page3, typeof(Page3));

            // test
            navigator.Forward(Pages.Page1);
            navigator.Push(Pages.Page2);
            Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(Pages.Page3, 0));
        }

        public enum Pages
        {
            Page1,
            Page2,
            Page3,
            Page4
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

        public class Page4 : MockPage
        {
        }
    }
}
