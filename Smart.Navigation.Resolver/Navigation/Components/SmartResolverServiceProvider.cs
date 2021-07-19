namespace Smart.Navigation.Components
{
    using System;

    using Smart.Resolver;

    public sealed class SmartResolverServiceProvider : IServiceProvider
    {
        private readonly SmartResolver resolver;

        public SmartResolverServiceProvider(SmartResolver resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType) => resolver.Get(serviceType);
    }
}
