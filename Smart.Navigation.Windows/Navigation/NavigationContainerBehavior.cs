namespace Smart.Navigation;

using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

[TypeConstraint(typeof(Canvas))]
public sealed class NavigationContainerBehavior : Behavior<Canvas>
{
    public static readonly DependencyProperty NavigatorProperty = DependencyProperty.Register(
        nameof(Navigator),
        typeof(INavigator),
        typeof(NavigationContainerBehavior),
        new PropertyMetadata(default(INavigator)));

    public INavigator Navigator
    {
        get => (INavigator)GetValue(NavigatorProperty);
        set => SetValue(NavigatorProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        AttachContainer(AssociatedObject);
    }

    protected override void OnDetaching()
    {
        AttachContainer(null);

        base.OnDetaching();
    }

    private void AttachContainer(Canvas? canvas)
    {
        if (Navigator is INavigatorComponentSource componentSource)
        {
            var updateContainer = componentSource.Components.Get<IUpdateContainer>();
            updateContainer.Attach(canvas);
        }
    }
}
