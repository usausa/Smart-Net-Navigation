namespace Smart.Navigation.Components
{
    using System;

    public sealed class StandardActivator : IActivator
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
