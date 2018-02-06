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
                .UseMockFormProvider()
                .ToNavigator();
            navigator.Confirm += (sender, args) =>
            {
                args.Cancel = args.Context.Parameter.GetValue<bool>("Cancel");
            };

            // test
            Assert.False(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("Cancel", true)));
            Assert.True(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("Cancel", false)));
        }

        [Fact]
        public static void TestNavigatorCanceldByInterface()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(CancelForm));

            Assert.False(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", false)));
            Assert.True(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", true)));
        }

        [Fact]
        public static async Task TestNavigatorCanceldByAsyncInterface()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            await navigator.ForwardAsync(typeof(CancelForm));

            Assert.False(await navigator.ForwardAsync(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", false)));
            Assert.True(await navigator.ForwardAsync(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", true)));
        }

        [Fact]
        public static async Task TestNavigatorCanceldByAsyncInterface2()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            await navigator.ForwardAsync(typeof(CancelAsyncForm));

            Assert.False(await navigator.ForwardAsync(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", false)));
            Assert.True(await navigator.ForwardAsync(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", true)));
        }

        public class ToForm : MockForm
        {
        }

        public class CancelForm : MockForm, IConfirmRequest
        {
            public bool CanNavigate(INavigationContext context)
            {
                return context.Parameter.GetValue<bool>("CanNavigate");
            }
        }

        public class CancelAsyncForm : MockForm, IConfirmRequestAsync
        {
            public async Task<bool> CanNavigateAsync(INavigationContext context)
            {
                await Task.Delay(0);
                return context.Parameter.GetValue<bool>("CanNavigate");
            }
        }
    }
}
