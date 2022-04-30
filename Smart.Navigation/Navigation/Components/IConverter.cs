namespace Smart.Navigation.Components;

public interface IConverter
{
    object? Convert(object? value, Type type);
}
