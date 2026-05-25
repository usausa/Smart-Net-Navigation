namespace Example.WindowsApp.Plugins;

using Example.WindowsApp.Animation;
using Example.WindowsApp.Views;

using Smart.Navigation;
using Smart.Navigation.Plugins;

internal sealed class DialogEffectPlugin : PluginBase
{
    private static readonly Type DialogViewType = typeof(IDialogView);

    public override void OnPrepareParameter(
        IPluginContext pluginContext,
        INavigationContext navigationContext,
        INavigationParameterPrepare parameter)
    {
        if (parameter.Effect is not null)
        {
            return;
        }

        // Open effect
        var toType = navigationContext.ToId as Type;
        var toIsDialog = toType is not null && DialogViewType.IsAssignableFrom(toType);
        if (toIsDialog)
        {
            parameter.WithEffect(ExampleEffect.DialogOpen);
            return;
        }

        // Close efec
        var fromType = navigationContext.FromId as Type;
        var fromIsDialog = fromType is not null && DialogViewType.IsAssignableFrom(fromType);
        if (fromIsDialog)
        {
            parameter.WithEffect(ExampleEffect.DialogClose);
        }
    }
}
