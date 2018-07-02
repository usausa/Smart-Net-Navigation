namespace Example.WindowsApp.Views
{
    using System.Windows;

    public static class ShellProperty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.Maintainability", "SA1401:FieldsMustBePrivate", Justification = "Ignore")]
        public static DependencyProperty ShellControlProperty = DependencyProperty.RegisterAttached(
            "ShellControl",
            typeof(IShellControl),
            typeof(ShellProperty),
            new PropertyMetadata(null));

        public static IShellControl GetShellControl(DependencyObject obj)
        {
            return (IShellControl)obj.GetValue(ShellControlProperty);
        }

        public static void SetShellControl(DependencyObject obj, IShellControl value)
        {
            obj.SetValue(ShellControlProperty, value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.Maintainability", "SA1401:FieldsMustBePrivate", Justification = "Ignore")]
        public static DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(
            "Title",
            typeof(string),
            typeof(ShellProperty),
            new PropertyMetadata(string.Empty, PropertyChanged));

        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var parent = ((FrameworkElement)d).Parent;
            if (parent == null)
            {
                return;
            }

            var shell = GetShellControl(parent);
            if (shell != null)
            {
                UpdateShellControl(shell, d);
            }
        }

        public static void UpdateShellControl(IShellControl shell, DependencyObject d)
        {
            shell.Title.Value = d == null ? string.Empty : GetTitle(d);
        }
    }
}
