namespace Smart.Navigation;

using System.Runtime.CompilerServices;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using Avalonia.VisualTree;

using Smart.Navigation.Components;

public sealed class AvaloniaNavigationProvider : IAsyncNavigationProvider
{
    private readonly IContainerResolver resolver;

    private readonly AvaloniaNavigationProviderOptions options;

    private readonly Dictionary<string, IAvaloniaNavigationEffect> effects;

    public AvaloniaNavigationProvider(IContainerResolver resolver, AvaloniaNavigationProviderOptions options, IEffectFactory? effectFactory = null)
    {
        this.resolver = resolver;
        this.options = options;
        effects = options.CreateEffects(effectFactory ?? new StandardEffectFactory());
    }

    // ------------------------------------------------------------
    // Resolve
    // ------------------------------------------------------------

    public object? ResolveTarget(object view)
    {
        return Unsafe.As<Control>(view).DataContext;
    }

    // ------------------------------------------------------------
    // Sync
    // ------------------------------------------------------------

    public void OpenView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        container.Children.Add(Unsafe.As<Control>(view));
    }

    public void CloseView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var element = Unsafe.As<Control>(view);
        var focusWithin = options.RestoreFocus && element.IsKeyboardFocusWithin;

        (element as IDisposable)?.Dispose();
        (element.DataContext as IDisposable)?.Dispose();
        element.DataContext = null;

        container.Children.Remove(element);

        if (focusWithin)
        {
            Dispatcher.UIThread.Post(() => FocusFirst(container), DispatcherPriority.Loaded);
        }
    }

    public void ActivateView(object view, object? parameter)
    {
        var element = Unsafe.As<Control>(view);

        element.IsVisible = true;

        var control = element;
        if (options.RestoreFocus)
        {
            if (parameter is IInputElement focused)
            {
                focused.Focus();
            }
            else
            {
                control.Focus();
            }
        }
    }

    public object? DeactivateView(object view)
    {
        var element = Unsafe.As<Control>(view);

        var parameter = options.RestoreFocus ? GetFocused(element) : null;

        element.IsVisible = false;

        return parameter;
    }

    // ------------------------------------------------------------
    // Async
    // ------------------------------------------------------------

    public async Task OpenViewAsync(object view, INavigationParameter parameter)
    {
        OpenView(view);
        await PlayWithGuardAsync(view, parameter, AvaloniaNavigationEffectPhase.Open).ConfigureAwait(true);
    }

    public async Task CloseViewAsync(object view, INavigationParameter parameter)
    {
        await PlayWithGuardAsync(view, parameter, AvaloniaNavigationEffectPhase.Close).ConfigureAwait(true);
        CloseView(view);
    }

    public async Task ActivateViewAsync(object view, object? state, INavigationParameter parameter)
    {
        ActivateView(view, state);
        await PlayWithGuardAsync(view, parameter, AvaloniaNavigationEffectPhase.Activate).ConfigureAwait(true);
    }

    public async Task<object?> DeactivateViewAsync(object view, INavigationParameter parameter)
    {
        await PlayWithGuardAsync(view, parameter, AvaloniaNavigationEffectPhase.Deactivate).ConfigureAwait(true);
        return DeactivateView(view);
    }

    private async Task PlayWithGuardAsync(object view, INavigationParameter parameter, AvaloniaNavigationEffectPhase phase)
    {
        var element = Unsafe.As<Control>(view);

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

    private Task EffectAsync(Control element, INavigationParameter parameter, AvaloniaNavigationEffectPhase phase)
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

        return effect.EffectAsync(new AvaloniaNavigationEffectContext
        {
            Container = container,
            View = element,
            Phase = phase,
            Parameter = parameter
        });
    }

    // ------------------------------------------------------------
    // Helper
    // ------------------------------------------------------------

    private static void FocusFirst(Grid container)
    {
        if ((TopLevel.GetTopLevel(container)?.FocusManager.GetFocusedElement() is Visual focused) && focused.IsAttachedToVisualTree())
        {
            return;
        }

        FocusManager.FindFirstFocusableElement(container)?.Focus();
    }

    private static IInputElement? GetFocused(Visual parent)
    {
        if (parent is InputElement { IsFocused: true } inputElement)
        {
            return inputElement;
        }

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var child in parent.GetVisualChildren())
        {
            var focused = GetFocused(child);
            if (focused is not null)
            {
                return focused;
            }
        }

        return null;
    }
}
