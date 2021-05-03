namespace Smart.Navigation.Components
{
    using System;

    using Smart.Converter;

    public sealed class SmartConverter : IConverter
    {
        private readonly IObjectConverter converter;

        public SmartConverter()
            : this(ObjectConverter.Default)
        {
        }

        public SmartConverter(IObjectConverter converter)
        {
            this.converter = converter;
        }

        public object? Convert(object? value, Type type)
        {
            return converter.Convert(value, type);
        }
    }
}
