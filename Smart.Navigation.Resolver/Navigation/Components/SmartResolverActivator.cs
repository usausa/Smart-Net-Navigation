namespace Smart.Navigation.Components
{
    using System;

    using Smart.Resolver;

    public sealed class SmartResolverActivator : IActivator
    {
        private readonly IResolver resolver;

        public SmartResolverActivator(IResolver resolver)
        {
            this.resolver = resolver;
        }

        public object Resolve(Type type) => resolver.Get(type);
    }
}
