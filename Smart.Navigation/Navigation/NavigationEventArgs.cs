namespace Smart.Navigation;

public sealed class NavigationEventArgs : EventArgs
{
    public INavigationContext Context { get; }

    public object? FromView { get; }

    public object? FromTarget { get; }

    public object ToView { get; }

    public object ToTarget { get; }

    public NavigationEventArgs(
        INavigationContext context,
        object? fromView,
        object? fromTarget,
        object toView,
        object toTarget)
    {
        Context = context;
        FromView = fromView;
        FromTarget = fromTarget;
        ToView = toView;
        ToTarget = toTarget;
    }
}
