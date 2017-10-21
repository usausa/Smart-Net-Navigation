namespace Smart.Navigation.Components
{
    using System;

    using Smart.Converter;

    /// <summary>
    ///
    /// </summary>
    public class StandardConverter : IConverter
    {
        private readonly ObjectConverter converter;

        /// <summary>
        ///
        /// </summary>
        public StandardConverter()
            : this(ObjectConverter.Default)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="converter"></param>
        public StandardConverter(ObjectConverter converter)
        {
            this.converter = converter;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Convert(object value, Type type)
        {
            return converter.Convert(value, type);
        }
    }
}
