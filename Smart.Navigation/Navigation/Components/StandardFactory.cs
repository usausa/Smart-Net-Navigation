namespace Smart.Navigation.Components
{
    using System;

    public sealed class StandardFactory : IFactory
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
