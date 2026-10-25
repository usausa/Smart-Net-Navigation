namespace Smart.Navigation.Effects;

using System.Windows;

internal static class EffectHelper
{
    public static void Restore(WindowsNavigationEffectContext context, Action<FrameworkElement> restore)
    {
        var view = context.View;
        if ((context.Phase is not (WindowsNavigationEffectPhase.Close or WindowsNavigationEffectPhase.Deactivate)) || !view.IsVisible)
        {
            restore(view);
            return;
        }

        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            view.IsVisibleChanged -= OnIsVisibleChanged;
            restore(view);
        }

        view.IsVisibleChanged += OnIsVisibleChanged;
    }
}
