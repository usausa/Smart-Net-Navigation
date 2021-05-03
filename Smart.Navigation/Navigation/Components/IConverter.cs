namespace Smart.Navigation.Components
{
    using System;

    public interface IConverter
    {
        object? Convert(object? value, Type type);
    }
}
