namespace Smart.Navigation.Components;

public interface IActivator
{
    object Create(Type type);
}
