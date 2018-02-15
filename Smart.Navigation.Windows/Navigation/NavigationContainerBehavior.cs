namespace Smart.Navigation
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    [TypeConstraint(typeof(ContentControl))]
    public class NavigationContainerBehavior : Behavior<ContentControl>
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

            AttachContainer();
        }

        protected override void OnDetaching()
        {
            AttachContainer();

            base.OnDetaching();
        }

        private void AttachContainer()
        {
            if (Navigator is INavigatorComponentSource componentSource)
            {
                var updateContiner = componentSource.Components.Get<IUpdateContainer>();
                updateContiner.Attach(AssociatedObject);
            }
        }
    }
}
