namespace Smart.Navigation
{
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins.Resolver;
    using Smart.Resolver;

    public static class ResolverNavigatorConfigExtensions
    {
        public static NavigatorConfig UseResolver(this NavigatorConfig config, SmartResolver resolver)
        {
            config.UseServiceProvider(new SmartResolverServiceProvider(resolver));
            config.AddPlugin<ResolverPlugin>();
            return config;
        }
    }
}
