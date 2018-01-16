namespace Smart.Navigation
{
    using Smart.Mock;
    using Smart.Resolver;

    using Xunit;

    public class NavigatorNotifyTest
    {
        // ------------------------------------------------------------
        // Page
        // ------------------------------------------------------------

        [Fact]
        public static void TestPageNavigatorNotify()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.NotifyPage, typeof(NotifyPage));

            // test
            navigator.Forward(Pages.NotifyPage);
            navigator.Notify("test");

            var notifyPage = (NotifyPage)navigator.CurrentPage;
            Assert.Equal("test", notifyPage.Parameter);
        }

        [Fact]
        public static void TestPageNavigatorNotifyUnsuported()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.UnsupportPage, typeof(UnsupportPage));

            // test
            navigator.Forward(Pages.UnsupportPage);
            navigator.Notify("test");
        }

        public enum Pages
        {
            NotifyPage,
            UnsupportPage
        }

        public class NotifyPage : MockPage, INotifySupport
        {
            public object Parameter { get; private set; }

            public void NavigatorNotify(object parameter)
            {
                Parameter = parameter;
            }
        }

        public class UnsupportPage
        {
        }

        // ------------------------------------------------------------
        // View
        // ------------------------------------------------------------

        [Fact]
        public static void TestViewNavigatorNotify()
        {
            // prepare
            var resolver = new ResolverConfig()
                .UseAutoBinding()
                .ToResolver();
            var navigator = new NavigatorConfig()
                .UseMockViewProvider()
                .UseResolver(resolver)
                .ToNavigator();

            navigator.Register(Views.NotifyView, typeof(NotifyView));

            // test
            navigator.Forward(Views.NotifyView);
            navigator.Notify("test");

            var notifyView = (NotifyView)navigator.CurrentPage;
            var notifyViewModel = (NotifyViewModel)notifyView.Context;
            Assert.Equal("test", notifyViewModel.Parameter);
        }

        public enum Views
        {
            NotifyView
        }

        public class NotifyViewModel : INotifySupport
        {
            public object Parameter { get; private set; }

            public void NavigatorNotify(object parameter)
            {
                Parameter = parameter;
            }
        }

        public class NotifyView : MockView
        {
            public NotifyView(NotifyViewModel vm)
            {
                Context = vm;
            }
        }
    }
}
