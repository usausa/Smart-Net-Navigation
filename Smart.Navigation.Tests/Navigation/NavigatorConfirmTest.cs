namespace Smart.Navigation
{
    using Smart.Mock;

    using Xunit;

    public class NavigatorConfirmTest
    {
        [Fact]
        public static void TestNavigatorCanceldByEvent()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();
            navigator.Confirm += (sender, args) =>
            {
                args.Cancel = args.Context.Parameter.GetValue<bool>("Cancel");
            };

            navigator.Register(Pages.Page1, typeof(Page1));

            // test
            Assert.False(navigator.Forward(Pages.Page1, new NavigationParameter().SetValue("Cancel", true)));
            Assert.True(navigator.Forward(Pages.Page1, new NavigationParameter().SetValue("Cancel", false)));
        }

        [Fact]
        public static void TestNavigatorCanceldByInterface()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(CancelPage));
            navigator.Register(Pages.Page2, typeof(Page2));

            // test
            navigator.Forward(Pages.Page1);
            Assert.False(navigator.Forward(Pages.Page2, new NavigationParameter().SetValue("CanNavigate", false)));
            Assert.True(navigator.Forward(Pages.Page2, new NavigationParameter().SetValue("CanNavigate", true)));
        }

        public enum Pages
        {
            Page1,
            Page2
        }

        public class Page1 : MockPage
        {
        }

        public class Page2 : MockPage
        {
        }

        public class CancelPage : MockPage, IConfirmRequest
        {
            public bool CanNavigate(INavigationContext context)
            {
                return context.Parameter.GetValue<bool>("CanNavigate");
            }
        }
    }
}
