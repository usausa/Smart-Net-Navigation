﻿namespace Smart.Navigation
{
    using System;

    using Xamarin.Forms;

    public class NavigationContainerBehavior : Behavior<AbsoluteLayout>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BindableProperty")]
        public static readonly BindableProperty NavigatorProperty =
            BindableProperty.Create(nameof(Navigator), typeof(INavigator), typeof(NavigationContainerBehavior));

        public INavigator Navigator
        {
            get => (INavigator)GetValue(NavigatorProperty);
            set => SetValue(NavigatorProperty, value);
        }

        public AbsoluteLayout AssociatedObject { get; private set; }

        protected override void OnAttachedTo(AbsoluteLayout bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.SizeChanged += BindableOnSizeChanged;
            bindable.BindingContextChanged += HandleBindingContextChanged;

            AttachContainer(bindable);
        }

        protected override void OnDetachingFrom(AbsoluteLayout bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.SizeChanged -= BindableOnSizeChanged;
            bindable.BindingContextChanged -= HandleBindingContextChanged;
            BindingContext = null;

            AttachContainer(null);
        }

        private void BindableOnSizeChanged(object sender, EventArgs e)
        {
            if (Navigator?.CurrentView is View view)
            {
                view.WidthRequest = AssociatedObject.Width;
                view.HeightRequest = AssociatedObject.Height;
            }
        }

        private void HandleBindingContextChanged(object sender, EventArgs eventArgs)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            BindingContext = AssociatedObject.BindingContext;

            AttachContainer(AssociatedObject);
        }

        private void AttachContainer(AbsoluteLayout layout)
        {
            if (Navigator is INavigatorComponentSource componentSource)
            {
                var updateContiner = componentSource.Components.Get<IUpdateContainer>();
                updateContiner.Attach(layout);
            }
        }
    }
}
