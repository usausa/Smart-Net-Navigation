namespace Smart.Navigation;

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

public sealed class NavigationContainer
{
    public static readonly DependencyProperty NavigatorProperty = DependencyProperty.RegisterAttached(
        "Navigator",
        typeof(INavigator),
        typeof(NavigationContainer),
        new PropertyMetadata(HandlePropertyChanged));

    public static INavigator GetNavigator(DependencyObject obj)
    {
        return (INavigator)obj.GetValue(NavigatorProperty);
    }

    public static void SetNavigator(DependencyObject obj, INavigator value)
    {
        obj.SetValue(NavigatorProperty, value);
    }

    private static void HandlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(d))
        {
            return;
        }

        if ((d is Canvas canvas) && (e.NewValue is INavigatorComponentSource componentSource))
        {
            var updateContainer = componentSource.Components.Get<IUpdateContainer>();
            updateContainer.Attach(canvas);
        }
    }
}
