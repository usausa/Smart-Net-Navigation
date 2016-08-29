namespace Smart.Navigation
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class DefaultServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }
    }
}
