namespace Smart.Navigation.Components
{
    using System;

    using Smart.Converter;

    public sealed class StandardConverter : IConverter
    {
        private readonly ObjectConverter converter;

        public StandardConverter()
            : this(ObjectConverter.Default)
        {
        }

        public StandardConverter(ObjectConverter converter)
        {
            this.converter = converter;
        }

        public object Convert(object value, Type type)
        {
            return converter.Convert(value, type);
        }
    }
}
