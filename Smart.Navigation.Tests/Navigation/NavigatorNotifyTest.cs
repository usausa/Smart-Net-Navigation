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
        public static void FormNotify()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(NotifyForm));
            navigator.Notify(1);

            var notifyForm = (NotifyForm)navigator.CurrentView!;
            Assert.Equal(1, notifyForm.IntParameter);
        }

        [Fact]
        public static void FormNotifyUnsupported()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(UnsupportedForm));
            navigator.Notify("test");
            navigator.Notify(1);
        }

        public class NotifyForm : MockForm, INotifySupport<int>
        {
            public int IntParameter { get; private set; }

            public void NavigatorNotify(int parameter)
            {
                IntParameter = parameter;
            }
        }

        public class UnsupportedForm : MockForm
        {
        }

        // ------------------------------------------------------------
        // View
        // ------------------------------------------------------------

        [Fact]
        public static void ViewNotify()
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
            navigator.Notify(1);

            var notifyView = (NotifyWindow)navigator.CurrentView!;
            var notifyViewModel = (NotifyWindowViewModel)notifyView.Context;
            Assert.Equal(1, notifyViewModel.IntParameter);
        }

        public class NotifyWindowViewModel : INotifySupport<int>
        {
            public int IntParameter { get; private set; }

            public void NavigatorNotify(int parameter)
            {
                IntParameter = parameter;
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
