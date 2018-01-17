namespace Smart.Navigation
{
    using System.Threading.Tasks;

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

            navigator.Register(Pages.ToPage, typeof(ToPage));

            // test
            Assert.False(navigator.Forward(Pages.ToPage, new NavigationParameter().SetValue("Cancel", true)));
            Assert.True(navigator.Forward(Pages.ToPage, new NavigationParameter().SetValue("Cancel", false)));
        }

        [Fact]
        public static void TestNavigatorCanceldByInterface()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.CancelPage, typeof(CancelPage));
            navigator.Register(Pages.ToPage, typeof(ToPage));

            // test
            navigator.Forward(Pages.CancelPage);

            Assert.False(navigator.Forward(Pages.ToPage, new NavigationParameter().SetValue("CanNavigate", false)));
            Assert.True(navigator.Forward(Pages.ToPage, new NavigationParameter().SetValue("CanNavigate", true)));
        }

        [Fact]
        public static async Task TestNavigatorCanceldByAsyncInterface()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();

            navigator.Register(Pages.CancelPage, typeof(CancelAsyncPage));
            navigator.Register(Pages.ToPage, typeof(ToPage));

            // test
            await navigator.ForwardAsync(Pages.CancelPage);

            Assert.False(await navigator.ForwardAsync(Pages.ToPage, new NavigationParameter().SetValue("CanNavigate", false)));
            Assert.True(await navigator.ForwardAsync(Pages.ToPage, new NavigationParameter().SetValue("CanNavigate", true)));
        }

        public enum Pages
        {
            ToPage,
            CancelPage,
            CancelAsyncPage
        }

        public class ToPage : MockPage
        {
        }

        public class CancelPage : MockPage, IConfirmRequest
        {
            public bool CanNavigate(INavigationContext context)
            {
                return context.Parameter.GetValue<bool>("CanNavigate");
            }
        }

        public class CancelAsyncPage : MockPage, IConfirmRequestAsync
        {
            public async Task<bool> CanNavigateAsync(INavigationContext context)
            {
                await Task.Delay(0);
                return context.Parameter.GetValue<bool>("CanNavigate");
            }
        }
    }
}
