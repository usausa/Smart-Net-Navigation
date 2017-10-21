namespace Smart.Navigation.Components
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class StandardActivator : IActivator
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Create(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
