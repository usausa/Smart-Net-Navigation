namespace Smart.Navigation;

using Smart.Navigation.Strategies;

public static class NavigatorExtensions
{
    // ------------------------------------------------------------
    // Notify
    // ------------------------------------------------------------

    public static void Notify<T>(this INavigator navigator, T parameter)
    {
        if (navigator.CurrentTarget is INotifySupport<T> notifySupport)
        {
            notifySupport.NavigatorNotify(parameter);
        }
    }

    public static Task NotifyAsync<T>(this INavigator navigator, T parameter)
    {
        if (navigator.CurrentTarget is INotifySupportAsync<T> notifySupportAsyncT)
        {
            return notifySupportAsyncT.NavigatorNotifyAsync(parameter);
        }

        if (navigator.CurrentTarget is INotifySupport<T> notifySupport)
        {
            notifySupport.NavigatorNotify(parameter);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }

    // ------------------------------------------------------------
    // Forward
    // ------------------------------------------------------------

    public static bool Forward(this INavigator navigator, object id)
    {
        return navigator.Navigate(new ForwardStrategy(id), null);
    }

    public static bool Forward(this INavigator navigator, object id, INavigationParameter? parameter)
    {
        return navigator.Navigate(new ForwardStrategy(id), parameter);
    }

    // Async

    public static Task<bool> ForwardAsync(this INavigator navigator, object id)
    {
        return navigator.NavigateAsync(new ForwardStrategy(id), null);
    }

    public static Task<bool> ForwardAsync(this INavigator navigator, object id, INavigationParameter? parameter)
    {
        return navigator.NavigateAsync(new ForwardStrategy(id), parameter);
    }

    // ------------------------------------------------------------
    // Push
    // ------------------------------------------------------------

    public static bool Push(this INavigator navigator, object id)
    {
        return navigator.Navigate(new PushStrategy(id), null);
    }

    public static bool Push(this INavigator navigator, object id, INavigationParameter? parameter)
    {
        return navigator.Navigate(new PushStrategy(id), parameter);
    }

    // Async

    public static Task<bool> PushAsync(this INavigator navigator, object id)
    {
        return navigator.NavigateAsync(new PushStrategy(id), null);
    }

    public static Task<bool> PushAsync(this INavigator navigator, object id, INavigationParameter? parameter)
    {
        return navigator.NavigateAsync(new PushStrategy(id), parameter);
    }

    // ------------------------------------------------------------
    // Pop
    // ------------------------------------------------------------

    public static bool Pop(this INavigator navigator)
    {
        return navigator.Navigate(new PopStrategy(1), null);
    }

    public static bool Pop(this INavigator navigator, INavigationParameter? parameter)
    {
        return navigator.Navigate(new PopStrategy(1), parameter);
    }

    public static bool Pop(this INavigator navigator, int level)
    {
        return navigator.Navigate(new PopStrategy(level), null);
    }

    public static bool Pop(this INavigator navigator, int level, INavigationParameter? parameter)
    {
        return navigator.Navigate(new PopStrategy(level), parameter);
    }

    // Async

    public static Task<bool> PopAsync(this INavigator navigator)
    {
        return navigator.NavigateAsync(new PopStrategy(1), null);
    }

    public static Task<bool> PopAsync(this INavigator navigator, INavigationParameter? parameter)
    {
        return navigator.NavigateAsync(new PopStrategy(1), parameter);
    }

    public static Task<bool> PopAsync(this INavigator navigator, int level)
    {
        return navigator.NavigateAsync(new PopStrategy(level), null);
    }

    public static Task<bool> PopAsync(this INavigator navigator, int level, INavigationParameter? parameter)
    {
        return navigator.NavigateAsync(new PopStrategy(level), parameter);
    }

    // ------------------------------------------------------------
    // PopAndForward
    // ------------------------------------------------------------

    public static bool PopAndForward(this INavigator navigator, object id)
    {
        return navigator.Navigate(new PopAndForwardStrategy(id, 1), null);
    }

    public static bool PopAndForward(this INavigator navigator, object id, int level)
    {
        return navigator.Navigate(new PopAndForwardStrategy(id, level), null);
    }

    public static bool PopAllAndForward(this INavigator navigator, object id)
    {
        return navigator.Navigate(new PopAndForwardStrategy(id), null);
    }

    public static bool PopAndForward(this INavigator navigator, object id, INavigationParameter? parameter)
    {
        return navigator.Navigate(new PopAndForwardStrategy(id, 1), parameter);
    }

    public static bool PopAndForward(this INavigator navigator, object id, int level, INavigationParameter? parameter)
    {
        return navigator.Navigate(new PopAndForwardStrategy(id, level), parameter);
    }

    public static bool PopAllAndForward(this INavigator navigator, object id, INavigationParameter? parameter)
    {
        return navigator.Navigate(new PopAndForwardStrategy(id), parameter);
    }

    // Async

    public static Task<bool> PopAndForwardAsync(this INavigator navigator, object id)
    {
        return navigator.NavigateAsync(new PopAndForwardStrategy(id, 1), null);
    }

    public static Task<bool> PopAndForwardAsync(this INavigator navigator, object id, int level)
    {
        return navigator.NavigateAsync(new PopAndForwardStrategy(id, level), null);
    }

    public static Task<bool> PopAllAndForwardAsync(this INavigator navigator, object id)
    {
        return navigator.NavigateAsync(new PopAndForwardStrategy(id), null);
    }

    public static Task<bool> PopAndForwardAsync(this INavigator navigator, object id, INavigationParameter? parameter)
    {
        return navigator.NavigateAsync(new PopAndForwardStrategy(id, 1), parameter);
    }

    public static Task<bool> PopAndForwardAsync(this INavigator navigator, object id, int level, INavigationParameter? parameter)
    {
        return navigator.NavigateAsync(new PopAndForwardStrategy(id, level), parameter);
    }

    public static Task<bool> PopAllAndForwardAsync(this INavigator navigator, object id, INavigationParameter? parameter)
    {
        return navigator.NavigateAsync(new PopAndForwardStrategy(id), parameter);
    }
}
