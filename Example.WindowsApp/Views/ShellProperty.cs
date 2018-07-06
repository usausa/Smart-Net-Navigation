﻿namespace Example.WindowsApp.Views
{
    using System.Windows;

    public static class ShellProperty
    {
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
            var parent = ((FrameworkElement)d).Parent as FrameworkElement;
            if (parent == null)
            {
                return;
            }

            if (parent.DataContext is IShellControl shell)
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