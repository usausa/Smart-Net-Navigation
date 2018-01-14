namespace Smart.Navigation
{
    using Smart.Navigation.Components;
    using Smart.Resolver;

    public static class ResolverNavigatorConfigExtensions
    {
        public static NavigatorConfig UseResolver(this NavigatorConfig config, IResolver resolver)
        {
            return config.UseFactory(new SmartResolverFactory(resolver));
        }
    }
}
