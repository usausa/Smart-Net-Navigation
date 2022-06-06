namespace Example.MobileApp.Markup;

using Example.MobileApp.Modules;

[ContentProperty("Value")]
public sealed class ViewIdExtension : IMarkupExtension
{
    public ViewId Value { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider) => Value;
}
