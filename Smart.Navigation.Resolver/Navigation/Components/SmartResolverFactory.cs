namespace Smart.Navigation.Components
{
    using System;

    using Smart.Resolver;

    public sealed class SmartResolverFactory : IFactory
    {
        private readonly IResolver resolver;

        public SmartResolverFactory(IResolver resolver)
        {
            this.resolver = resolver;
        }

        public object Create(Type type)
        {
            return resolver.Get(type);
        }
    }
}
