namespace Smart.Navigation
{
    using System;

    using Xamarin.Forms;

    public class NavigationContainerBehavior : Behavior<AbsoluteLayout>
    {
        public static readonly BindableProperty NavigatorProperty =
            BindableProperty.Create(nameof(Navigator), typeof(INavigator), typeof(NavigationContainerBehavior));

        public INavigator Navigator
        {
            get => (INavigator)GetValue(NavigatorProperty);
            set => SetValue(NavigatorProperty, value);
        }

        public AbsoluteLayout? AssociatedObject { get; private set; }

        protected override void OnAttachedTo(AbsoluteLayout bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;

            if (bindable.BindingContext is not null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += HandleBindingContextChanged;

            AttachContainer(bindable);
        }

        protected override void OnDetachingFrom(AbsoluteLayout bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= HandleBindingContextChanged;
            AssociatedObject = null;

            AttachContainer(null);
        }

        private void HandleBindingContextChanged(object sender, EventArgs eventArgs)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            BindingContext = AssociatedObject?.BindingContext;

            AttachContainer(AssociatedObject);
        }

        private void AttachContainer(AbsoluteLayout? layout)
        {
            if (Navigator is INavigatorComponentSource componentSource)
            {
                var updateContainer = componentSource.Components.Get<IUpdateContainer>();
                updateContainer.Attach(layout);
            }
        }
    }
}
