namespace Smart.Navigation
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    public class NavigationContainerBehavior : Behavior<ContentControl>
    {
        public static readonly DependencyProperty NavigatorProperty = DependencyProperty.Register(
            nameof(NavigationContainerBehavior),
            typeof(INavigator),
            typeof(ContentControl));

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

        private void AttachContainer(ContentControl container)
        {
            if (container == null)
            {
                return;
            }

            if (Navigator is INavigatorComponentSource componentSource)
            {
                var updateContiner = componentSource.Components.Get<IUpdateContainer>();
                updateContiner.Attach(container);
            }
        }
    }
}
