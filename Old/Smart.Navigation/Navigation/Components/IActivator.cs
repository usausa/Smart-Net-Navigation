namespace Smart.Navigation.Components
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Create(Type type);
    }
}
