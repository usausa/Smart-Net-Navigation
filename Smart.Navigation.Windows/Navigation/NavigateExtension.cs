namespace Smart.Navigation
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Markup;

    [MarkupExtensionReturnType(typeof(ICommand))]
    public sealed class NavigateExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var pvt = serviceProvider as IProvideValueTarget;
            if (pvt == null)
            {
                return null;
            }

            var element = pvt.TargetObject as FrameworkElement;
            if (element == null)
            {
                return null;
            }

            var aware = element.DataContext as INavigatorAware;
            if (aware == null)
            {
                return null;
            }

            throw new NotImplementedException();
        }
    }
}
