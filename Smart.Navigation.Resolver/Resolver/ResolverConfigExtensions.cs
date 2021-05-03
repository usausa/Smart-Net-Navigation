namespace Smart.Resolver
{
    using Smart.Navigation.Components;
    using Smart.Resolver.Configs;
    using Smart.Resolver.Scopes;

    public static class ResolverConfigExtensions
    {
        public static ResolverConfig UsePageContextScope(this ResolverConfig config)
        {
            config.Bind<PageContextStorage>().ToSelf().InSingletonScope();
            config.Bind<PageContextKeyManager>().ToSelf().InSingletonScope();

            return config;
        }

        public static IBindingNamedWithSyntax InPageContextScope(this IBindingInSyntax syntax, string name)
        {
            return syntax.InScope(_ => new PageContextScope(name));
        }
    }
}
