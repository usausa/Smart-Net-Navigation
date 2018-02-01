namespace Smart.Navigation
{
    using Xamarin.Forms;

    public class NavigationContainerBehavior : Behavior<ContentView>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BindableProperty")]
        public static readonly BindableProperty NavigatorProperty =
            BindableProperty.Create(nameof(Navigator), typeof(INavigator), typeof(NavigationContainerBehavior));

        public INavigator Navigator
        {
            get => (INavigator)GetValue(NavigatorProperty);
            set => SetValue(NavigatorProperty, value);
        }

        protected override void OnAttachedTo(ContentView bindable)
        {
            base.OnAttachedTo(bindable);

            AttachContainer(bindable);
        }

        protected override void OnDetachingFrom(ContentView bindable)
        {
            AttachContainer(null);

            base.OnDetachingFrom(bindable);
        }

        private void AttachContainer(ContentView container)
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
