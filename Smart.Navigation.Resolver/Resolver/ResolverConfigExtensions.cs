namespace Smart.Resolver
{
    using System;

    using Smart.Navigation.Components;
    using Smart.Resolver.Configs;
    using Smart.Resolver.Scopes;

    public static class ResolverConfigExtensions
    {
        public static ResolverConfig UsePageContextScope(this ResolverConfig config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config.Bind<PageContextStorage>().ToSelf().InSingletonScope();

            return config;
        }

        public static IBindingNamedWithSyntax InPageContextScope(this IBindingInSyntax syntax, string name)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return syntax.InScope(c => new PageContextScope(name));
        }
    }
}
