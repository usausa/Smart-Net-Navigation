namespace Example.WindowsApp.Views
{
    using System;
    using System.Windows;

    using Microsoft.Xaml.Behaviors;

    using Smart.Navigation;

    [TypeConstraint(typeof(Window))]
    public sealed class ShellUpdateBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty NavigatorProperty = DependencyProperty.Register(
            nameof(Navigator),
            typeof(INavigator),
            typeof(ShellUpdateBehavior),
            new PropertyMetadata(default(INavigator)));

        public INavigator Navigator
        {
            get => (INavigator)GetValue(NavigatorProperty);
            set => SetValue(NavigatorProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            Navigator.Navigated += NavigatorOnNavigated;
            Navigator.Exited += NavigatorOnExited;
        }

        protected override void OnDetaching()
        {
            Navigator.Navigated -= NavigatorOnNavigated;
            Navigator.Exited -= NavigatorOnExited;

            base.OnDetaching();
        }

        private void NavigatorOnNavigated(object? sender, NavigationEventArgs e)
        {
            UpdateShell(e.ToView);
        }

        private void NavigatorOnExited(object? sender, EventArgs e)
        {
            UpdateShell(null);
        }

        private void UpdateShell(object? view)
        {
            if (AssociatedObject.DataContext is IShellControl shell)
            {
                ShellProperty.UpdateShellControl(shell, view as DependencyObject);
            }
        }
    }
}
