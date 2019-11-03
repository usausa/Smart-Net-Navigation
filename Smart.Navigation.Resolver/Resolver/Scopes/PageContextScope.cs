namespace Smart.Resolver.Scopes
{
    using System;

    using Smart.ComponentModel;
    using Smart.Navigation.Components;
    using Smart.Resolver.Bindings;

    public sealed class PageContextScope : IScope
    {
        private readonly string name;

        private PageContextStorage storage;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Ignore")]
        public PageContextScope(string name)
        {
            this.name = name;
        }

        public IScope Copy(IComponentContainer components)
        {
            return new PageContextScope(name);
        }

        public Func<IResolver, object> Create(IBinding binding, Func<object> factory)
        {
            return k =>
            {
                if (storage is null)
                {
                    storage = k.Get<PageContextStorage>();
                }

                return storage.Resolve(name, binding, factory);
            };
        }
    }
}
