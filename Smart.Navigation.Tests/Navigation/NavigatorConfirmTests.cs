namespace Smart.Navigation;

using Smart.Mock;

public sealed class NavigatorConfirmTests
{
    [Fact]
    public static void CanceledByEvent()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();
        navigator.Confirm += static (_, args) =>
        {
            args.Cancel = args.Context.Parameter.GetValue<bool>("Cancel");
        };

        // Act & Assert
        Assert.False(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("Cancel", true)));
        Assert.True(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("Cancel", false)));
    }

    [Fact]
    public static void CanceledByInterface()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        navigator.Forward(typeof(CancelForm));

        // Act & Assert
        Assert.False(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", false)));
        Assert.True(navigator.Forward(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", true)));
    }

    [Fact]
    public async Task TestNavigatorCanceledByAsyncInterface()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        await navigator.ForwardAsync(typeof(CancelForm));

        // Act & Assert
        Assert.False(await navigator.ForwardAsync(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", false)));
        Assert.True(await navigator.ForwardAsync(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", true)));
    }

    [Fact]
    public async Task TestNavigatorCanceledByAsyncInterface2()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        await navigator.ForwardAsync(typeof(CancelAsyncForm));

        // Act & Assert
        Assert.False(await navigator.ForwardAsync(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", false)));
        Assert.True(await navigator.ForwardAsync(typeof(ToForm), new NavigationParameter().SetValue("CanNavigate", true)));
    }

    [Fact]
    public async Task ConfirmEventCalledOnceByAsyncNavigation()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();
        var count = 0;
        navigator.Confirm += (_, _) => count++;

        // Act
        await navigator.ForwardAsync(typeof(ToForm));

        // Assert
        Assert.Equal(1, count);
    }

    public sealed class ToForm : MockForm;

    public sealed class CancelForm : MockForm, IConfirmRequest
    {
        public bool CanNavigate(INavigationContext context)
        {
            return context.Parameter.GetValue<bool>("CanNavigate");
        }
    }

    public sealed class CancelAsyncForm : MockForm, IConfirmRequestAsync
    {
        public async Task<bool> CanNavigateAsync(INavigationContext context)
        {
            await Task.Delay(0);
            return context.Parameter.GetValue<bool>("CanNavigate");
        }
    }
}
