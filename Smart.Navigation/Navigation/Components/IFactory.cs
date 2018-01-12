namespace Smart.Navigation.Components
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Create(Type type);
    }
}
