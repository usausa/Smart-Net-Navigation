﻿namespace Example.FormsApp.Converters
{
    using System;
    using System.Globalization;

    using Example.FormsApp.Views;

    using Xamarin.Forms;

    public sealed class ViewTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BindableObject bindable)
            {
                return ViewProperty.GetTitle(bindable);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
