namespace Example.FormsApp.Markup
{
    using System;

    using Example.FormsApp.Modules;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty("Value")]
    public sealed class ViewIdExtension : IMarkupExtension
    {
        public ViewId Value { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider) => Value;
    }
}
