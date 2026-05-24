namespace Smart.Navigation.Components;

using System.Diagnostics.CodeAnalysis;

using Smart.Converter;

[RequiresUnreferencedCode("SmartConverter uses IObjectConverter.Convert which relies on reflection to discover types at runtime.")]
[RequiresDynamicCode("SmartConverter uses IObjectConverter.Convert which uses MakeGenericType/MakeGenericMethod at runtime.")]
public sealed class SmartConverter : IConverter
{
    private readonly IObjectConverter converter;

    public SmartConverter()
        : this(ObjectConverter.Default)
    {
    }

    public SmartConverter(IObjectConverter converter)
    {
        this.converter = converter;
    }

    public object? Convert(object? value, Type type)
    {
        return converter.Convert(value, type);
    }
}
