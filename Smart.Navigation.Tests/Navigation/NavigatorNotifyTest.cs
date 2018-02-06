namespace Smart.Navigation
{
    using Smart.Mock;
    using Smart.Resolver;

    using Xunit;

    public class NavigatorNotifyTest
    {
        // ------------------------------------------------------------
        // View
        // ------------------------------------------------------------

        [Fact]
        public static void TestFormNavigatorNotify()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(NotifyForm));
            navigator.Notify("test");

            var notifyForm = (NotifyForm)navigator.CurrentView;
            Assert.Equal("test", notifyForm.Parameter);
        }

        [Fact]
        public static void TestFormNavigatorNotifyUnsuported()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(UnsupportForm));
            navigator.Notify("test");
        }

        public class NotifyForm : MockForm, INotifySupport
        {
            public object Parameter { get; private set; }

            public void NavigatorNotify(object parameter)
            {
                Parameter = parameter;
            }
        }

        public class UnsupportForm : MockForm
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
                .UseMockWindowProvider()
                .UseResolver(resolver)
                .ToNavigator();

            // test
            navigator.Forward(typeof(NotifyWindow));
            navigator.Notify("test");

            var notifyView = (NotifyWindow)navigator.CurrentView;
            var notifyViewModel = (NotifyWindowViewModel)notifyView.Context;
            Assert.Equal("test", notifyViewModel.Parameter);
        }

        public class NotifyWindowViewModel : INotifySupport
        {
            public object Parameter { get; private set; }

            public void NavigatorNotify(object parameter)
            {
                Parameter = parameter;
            }
        }

        public class NotifyWindow : MockWindow
        {
            public NotifyWindow(NotifyWindowViewModel vm)
            {
                Context = vm;
            }
        }
    }
}
