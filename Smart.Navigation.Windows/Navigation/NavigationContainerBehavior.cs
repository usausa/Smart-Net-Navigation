namespace Smart.Navigation
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    [TypeConstraint(typeof(Canvas))]
    public class NavigationContainerBehavior : Behavior<Canvas>
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

            AssociatedObject.SizeChanged += AssociatedObjectOnSizeChanged;

            AttachContainer(AssociatedObject);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SizeChanged -= AssociatedObjectOnSizeChanged;

            AttachContainer(null);

            base.OnDetaching();
        }

        private void AssociatedObjectOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Navigator?.CurrentView is FrameworkElement element)
            {
                element.Width = AssociatedObject.ActualWidth;
                element.Height = AssociatedObject.ActualHeight;
            }
        }

        private void AttachContainer(Canvas canvas)
        {
            if (Navigator is INavigatorComponentSource componentSource)
            {
                var updateContiner = componentSource.Components.Get<IUpdateContainer>();
                updateContiner.Attach(canvas);
            }
        }
    }
}
