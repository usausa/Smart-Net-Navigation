namespace Smart.Navigation;

using System.Runtime.CompilerServices;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

using Smart.Navigation.Components;

public sealed class WinUINavigationProvider : IAsyncNavigationProvider
{
    private readonly IContainerResolver resolver;

    private readonly WinUINavigationProviderOptions options;

    private readonly Dictionary<string, IWinUINavigationEffect> effects;

    public WinUINavigationProvider(IContainerResolver resolver, WinUINavigationProviderOptions options, IEffectFactory? effectFactory = null)
    {
        this.resolver = resolver;
        this.options = options;
        effects = options.CreateEffects(effectFactory ?? new StandardEffectFactory());
    }

    public object? ResolveTarget(object view)
    {
        return Unsafe.As<FrameworkElement>(view).DataContext;
    }

    public void OpenView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        if (options.ClipToBounds)
        {
            var visual = ElementCompositionPreview.GetElementVisual(container);
            visual.Clip ??= visual.Compositor.CreateInsetClip();
        }

        container.Children.Add(Unsafe.As<FrameworkElement>(view));
    }

    public void CloseView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var element = Unsafe.As<FrameworkElement>(view);

        (view as IDisposable)?.Dispose();
        (element.DataContext as IDisposable)?.Dispose();
        element.DataContext = null;

        container.Children.Remove(element);
    }

    public void ActivateView(object view, object? parameter)
    {
        var element = Unsafe.As<FrameworkElement>(view);

        element.Visibility = Visibility.Visible;

        if (options.RestoreFocus)
        {
            if (parameter is UIElement focused)
            {
                focused.Focus(FocusState.Programmatic);
            }
            else
            {
                element.Focus(FocusState.Programmatic);
            }
        }
    }

    public object? DeactivateView(object view)
    {
        var element = Unsafe.As<FrameworkElement>(view);

        var parameter = options.RestoreFocus ? GetFocused(element) : null;

        element.Visibility = Visibility.Collapsed;

        return parameter;
    }

    public async Task OpenViewAsync(object view, INavigationParameter parameter)
    {
        OpenView(view);
        await PlayWithGuardAsync(view, parameter, WinUINavigationEffectPhase.Open).ConfigureAwait(true);
    }

    public async Task CloseViewAsync(object view, INavigationParameter parameter)
    {
        await PlayWithGuardAsync(view, parameter, WinUINavigationEffectPhase.Close).ConfigureAwait(true);
        CloseView(view);
    }

    public async Task ActivateViewAsync(object view, object? state, INavigationParameter parameter)
    {
        ActivateView(view, state);
        await PlayWithGuardAsync(view, parameter, WinUINavigationEffectPhase.Activate).ConfigureAwait(true);
    }

    public async Task<object?> DeactivateViewAsync(object view, INavigationParameter parameter)
    {
        await PlayWithGuardAsync(view, parameter, WinUINavigationEffectPhase.Deactivate).ConfigureAwait(true);
        return DeactivateView(view);
    }

    private async Task PlayWithGuardAsync(object view, INavigationParameter parameter, WinUINavigationEffectPhase phase)
    {
        var element = Unsafe.As<FrameworkElement>(view);

        element.IsHitTestVisible = false;
        try
        {
            await EffectAsync(element, parameter, phase).ConfigureAwait(true);
        }
        finally
        {
            element.IsHitTestVisible = true;
        }
    }

    private Task EffectAsync(FrameworkElement element, INavigationParameter parameter, WinUINavigationEffectPhase phase)
    {
        var key = parameter.Effect;
        if ((key is null) || !effects.TryGetValue(key, out var effect))
        {
            return Task.CompletedTask;
        }

        var container = resolver.Container;
        if (container is null)
        {
            return Task.CompletedTask;
        }

        return effect.EffectAsync(new WinUINavigationEffectContext
        {
            Container = container,
            View = element,
            Phase = phase,
            Parameter = parameter
        });
    }

    private static UIElement? GetFocused(FrameworkElement parent)
    {
        if ((parent.XamlRoot is null) || (FocusManager.GetFocusedElement(parent.XamlRoot) is not UIElement focused))
        {
            return null;
        }

        for (DependencyObject? current = focused; current is not null; current = VisualTreeHelper.GetParent(current))
        {
            if (ReferenceEquals(current, parent))
            {
                return focused;
            }
        }

        return null;
    }
}
