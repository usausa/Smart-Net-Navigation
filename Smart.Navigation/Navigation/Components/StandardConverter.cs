namespace Smart.Navigation.Components
{
    using System;

    public sealed class StandardConverter : IConverter
    {
        public object Convert(object value, Type type)
        {
            return System.Convert.ChangeType(value, type);
        }
    }
}
