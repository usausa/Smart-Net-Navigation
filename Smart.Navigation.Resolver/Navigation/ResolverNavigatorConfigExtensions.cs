namespace Smart.Navigation
{
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins.Resolver;
    using Smart.Resolver;

    public static class ResolverNavigatorConfigExtensions
    {
        public static NavigatorConfig UseResolver(this NavigatorConfig config, IResolver resolver)
        {
            config.UseActivator(new SmartResolverActivator(resolver));
            config.AddPlugin<ResolverPlugin>();
            return config;
        }
    }
}
