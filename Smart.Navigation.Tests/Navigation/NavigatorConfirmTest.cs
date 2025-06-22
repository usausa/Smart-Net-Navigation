namespace Smart.Navigation;

using Smart.Mock;

public sealed class NavigatorConfirmTest
{
    [Fact]
    public static void CanceledByEvent()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();
        navigator.Confirm += static (_, args) =>
        {
            args.Cancel = args.Context.Parameter.GetValue<bool>("Cancel");
        };

        // test
        Assert.False(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("Cancel", true)));
        Assert.True(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("Cancel", false)));
    }

    [Fact]
    public static void CanceledByInterface()
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
    public async Task TestNavigatorCanceledByAsyncInterface()
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
    public async Task TestNavigatorCanceledByAsyncInterface2()
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

    public sealed class ToForm : MockForm
    {
    }

    public sealed class CancelForm : MockForm, IConfirmRequest
    {
        public bool CanNavigate(INavigationContext context)
        {
            return context.Parameter.GetValue<bool>("CanNavigate");
        }
    }

    public sealed class CancelAsyncForm : MockForm, IConfirmRequestAsync
    {
        public async ValueTask<bool> CanNavigateAsync(INavigationContext context)
        {
            await Task.Delay(0);
            return context.Parameter.GetValue<bool>("CanNavigate");
        }
    }
}
