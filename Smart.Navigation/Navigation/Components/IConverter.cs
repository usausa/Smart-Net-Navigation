namespace Smart.Navigation.Components
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object Convert(object value, Type type);
    }
}
