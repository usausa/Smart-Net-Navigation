namespace Example.WindowsApp.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using Example.WindowsApp.Views;

    [ValueConversion(typeof(object), typeof(string))]
    public sealed class ShellTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : ShellProperty.GetTitle((DependencyObject)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
