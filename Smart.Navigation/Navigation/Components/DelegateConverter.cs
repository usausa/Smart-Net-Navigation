namespace Smart.Navigation.Components;

public sealed class DelegateConverter : IConverter
{
    private readonly Func<object?, Type, object?> callback;

    public DelegateConverter(Func<object?, Type, object?> callback)
    {
        this.callback = callback;
    }

    public object? Convert(object? value, Type type) => callback(value, type);
}
