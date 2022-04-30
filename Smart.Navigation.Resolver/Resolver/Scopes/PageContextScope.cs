namespace Smart.Resolver.Scopes;

using Smart.ComponentModel;
using Smart.Navigation.Components;

public sealed class PageContextScope : IScope
{
    private readonly string name;

    private PageContextStorage? storage;

    private int key;

    public PageContextScope(string name)
    {
        this.name = name;
    }

    public IScope Copy(ComponentContainer components)
    {
        return new PageContextScope(name);
    }

    public Func<IResolver, object> Create(Func<object> factory)
    {
        return resolver =>
        {
            if (storage is null)
            {
                storage = resolver.Get<PageContextStorage>();
                key = resolver.Get<PageContextKeyManager>().Acquire();
            }

            return storage.Resolve(name, key, factory);
        };
    }
}
