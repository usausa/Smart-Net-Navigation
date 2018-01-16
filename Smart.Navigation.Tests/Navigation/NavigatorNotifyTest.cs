namespace Smart.Navigation
{
    using Smart.Mock;

    using Xunit;

    public class NavigatorNotifyTest
    {
        [Fact]
        public static void TestNavigatorNotify()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(Pages.NotifyPage, typeof(NotifyPage));

            // test
            navigator.Forward(Pages.NotifyPage);
            navigator.Notify("test");

            var notifyPage = (NotifyPage)navigator.CurrentPage;
            Assert.Equal("test", notifyPage.Parameter);
        }

        public enum Pages
        {
            NotifyPage
        }

        public class NotifyPage : MockPage, INotifySupport
        {
            public object Parameter { get; private set; }

            public void NavigatorNotify(object parameter)
            {
                Parameter = parameter;
            }
        }
    }
}
