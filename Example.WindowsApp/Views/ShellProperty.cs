namespace Example.WindowsApp.Views
{
    using System.Windows;

    public static class ShellProperty
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(
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
            if (((FrameworkElement)d).Parent is FrameworkElement { DataContext: IShellControl shell })
            {
                UpdateShellControl(shell, d);
            }
        }

        public static void UpdateShellControl(IShellControl shell, DependencyObject? d)
        {
            shell.Title.Value = d is null ? string.Empty : GetTitle(d);
        }
    }
}
