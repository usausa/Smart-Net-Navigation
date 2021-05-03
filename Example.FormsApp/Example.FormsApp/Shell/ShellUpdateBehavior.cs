namespace Example.FormsApp.Shell
{
    using System;

    using Smart.Forms.Interactivity;
    using Smart.Navigation;

    using Xamarin.Forms;

    public class ShellUpdateBehavior : BehaviorBase<ContentPage>
    {
        public static readonly BindableProperty NavigatorProperty =
            BindableProperty.Create(nameof(Navigator), typeof(INavigator), typeof(ShellUpdateBehavior));

        public INavigator Navigator
        {
            get => (INavigator)GetValue(NavigatorProperty);
            set => SetValue(NavigatorProperty, value);
        }

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            Navigator.Navigated += NavigatorOnNavigated;
            Navigator.Exited += NavigatorOnExited;
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            Navigator.Navigated -= NavigatorOnNavigated;
            Navigator.Exited -= NavigatorOnExited;

            base.OnDetachingFrom(bindable);
        }

        private void NavigatorOnNavigated(object sender, Smart.Navigation.NavigationEventArgs e)
        {
            UpdateShell(e.ToView);
        }

        private void NavigatorOnExited(object sender, EventArgs e)
        {
            UpdateShell(null);
        }

        private void UpdateShell(object? view)
        {
            if (AssociatedObject.BindingContext is IShellControl shell)
            {
                ShellProperty.UpdateShellControl(shell, view as BindableObject);
            }
        }
    }
}
