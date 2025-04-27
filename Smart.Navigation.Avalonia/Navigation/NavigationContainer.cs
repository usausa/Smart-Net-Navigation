namespace Smart.Navigation;

using Avalonia;
using Avalonia.Controls;

public static class NavigationContainer
{
    public static readonly AttachedProperty<INavigator?> NavigatorProperty =
        AvaloniaProperty.RegisterAttached<Canvas, INavigator?>("Navigator", typeof(NavigationContainer));

    public static INavigator? GetNavigator(Canvas canvas) =>
        canvas.GetValue(NavigatorProperty);

    public static void SetNavigator(Canvas canvas, INavigator? value) =>
        canvas.SetValue(NavigatorProperty, value);

    static NavigationContainer()
    {
        NavigatorProperty.Changed.AddClassHandler<Canvas>(OnNavigatorChanged);
    }

    private static void OnNavigatorChanged(Canvas canvas, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is INavigatorComponentSource componentSource)
        {
            var updateContainer = componentSource.Components.Get<IUpdateContainer>();
            updateContainer.Attach(canvas);
        }
    }
}
