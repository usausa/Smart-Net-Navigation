namespace Smart.Navigation.Effects;

using System.Runtime.CompilerServices;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;

internal static class EffectHelper
{
    public static Task AnimateAsync(WinUINavigationEffectContext context, DoubleAnimation animation, DependencyObject target, string path, Action<FrameworkElement> restore)
    {
        Storyboard.SetTarget(animation, target);
        Storyboard.SetTargetProperty(animation, path);

        var storyboard = new Storyboard();
        storyboard.Children.Add(animation);

        var tcs = new TaskCompletionSource();
        storyboard.Completed += (_, _) =>
        {
            Restore(context, view =>
            {
                storyboard.Stop();
                restore(view);
            });
            tcs.TrySetResult();
        };

        storyboard.Begin();
        return tcs.Task;
    }

    public static void Restore(WinUINavigationEffectContext context, Action<FrameworkElement> restore)
    {
        var view = context.View;
        if ((context.Phase == WinUINavigationEffectPhase.Deactivate) && (view.Visibility == Visibility.Visible))
        {
            var token = new StrongBox<long>();
            token.Value = view.RegisterPropertyChangedCallback(UIElement.VisibilityProperty, (_, _) =>
            {
                view.UnregisterPropertyChangedCallback(UIElement.VisibilityProperty, token.Value);
                restore(view);
            });
            return;
        }

        if ((context.Phase == WinUINavigationEffectPhase.Close) && view.IsLoaded)
        {
            void OnUnloaded(object sender, RoutedEventArgs e)
            {
                view.Unloaded -= OnUnloaded;
                restore(view);
            }

            view.Unloaded += OnUnloaded;
            return;
        }

        restore(view);
    }
}
