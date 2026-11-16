namespace Smart.Navigation;

using Avalonia;
using Avalonia.Controls;

public static class NavigationContainer
{
    public static readonly AttachedProperty<INavigator?> NavigatorProperty =
        AvaloniaProperty.RegisterAttached<Grid, INavigator?>("Navigator", typeof(NavigationContainer));

    public static INavigator? GetNavigator(Grid grid) =>
        grid.GetValue(NavigatorProperty);

    public static void SetNavigator(Grid grid, INavigator? value) =>
        grid.SetValue(NavigatorProperty, value);

    static NavigationContainer()
    {
        NavigatorProperty.Changed.AddClassHandler<Grid>(OnNavigatorChanged);
    }

    private static void OnNavigatorChanged(Grid grid, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is INavigatorComponentSource componentSource)
        {
            var updateContainer = componentSource.Components.Get<IUpdateContainer>();
            updateContainer.Attach(grid);
        }
    }
}
