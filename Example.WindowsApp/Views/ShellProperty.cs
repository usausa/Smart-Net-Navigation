namespace Example.WindowsApp.Views
{
    using System.Windows;

    public static class ShellProperty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.Maintainability", "SA1401:FieldsMustBePrivate", Justification = "Ignore")]
        public static DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(
            "Title",
            typeof(string),
            typeof(ShellProperty),
            new PropertyMetadata(string.Empty));

        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }
    }
}
