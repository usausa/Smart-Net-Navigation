namespace Smart.Navigation
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class DefaultNavigationResolver : INavigationResolver
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
