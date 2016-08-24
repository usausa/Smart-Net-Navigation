namespace Smart.Navigation
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public interface INavigationResolver
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);
    }
}
