namespace Smart.Navigation
{
    using Smart.ComponentModel;

    public interface INavigatorConfig
    {
        IComponentContainer ResolveComponents();
    }
}
