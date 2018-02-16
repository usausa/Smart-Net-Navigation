namespace Example.FormsApp.Converters
{
    using System;
    using System.Globalization;

    using Example.FormsApp.Views;

    using Xamarin.Forms;

    public sealed class ViewTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? ViewProperty.GetTitle((BindableObject)value) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
