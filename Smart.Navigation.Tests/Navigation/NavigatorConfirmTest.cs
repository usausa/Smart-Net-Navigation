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
                .UseMockProvider()
                .ToNavigator();
            navigator.Confirm += (sender, args) =>
            {
                args.Cancel = args.Context.Parameter.GetValue<bool>("Cancel");
            };

            navigator.Register(1, typeof(Page1));

            // test
            Assert.False(navigator.Forward(1, new NavigationParameter().SetValue("Cancel", true)));
            Assert.True(navigator.Forward(1, new NavigationParameter().SetValue("Cancel", false)));
        }

        [Fact]
        public static void TestNavigatorCanceldByInterface()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockProvider()
                .ToNavigator();

            navigator.Register(1, typeof(CancelPage));
            navigator.Register(2, typeof(Page2));

            // test
            navigator.Forward(1);
            Assert.False(navigator.Forward(2, new NavigationParameter().SetValue("CanNavigate", false)));
            Assert.True(navigator.Forward(2, new NavigationParameter().SetValue("CanNavigate", true)));
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
