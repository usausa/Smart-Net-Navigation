namespace Example.FormsApp.Converters
{
    using System;
    using System.Globalization;

    using Smart.Forms.ViewModels;

    using Xamarin.Forms;

    public sealed class ViewIsBusyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // TODO this is bad. behavior ?
            if (value is BindableObject bindable &&
                bindable.BindingContext is ViewModelBase vm)
            {
                return vm.IsBusy;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
