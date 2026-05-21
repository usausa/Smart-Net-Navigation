namespace Smart.Navigation;

using System.Runtime.CompilerServices;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.VisualTree;

public sealed class AvaloniaNavigationProvider : INavigationProvider, IAsyncNavigationProvider
{
    private readonly IContainerResolver resolver;

    private readonly AvaloniaNavigationProviderOptions options;

    public AvaloniaNavigationProvider(IContainerResolver resolver, AvaloniaNavigationProviderOptions options)
    {
        this.resolver = resolver;
        this.options = options;
    }

    public object? ResolveTarget(object view)
    {
        return Unsafe.As<Control>(view).DataContext;
    }

    public void OpenView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var element = Unsafe.As<Control>(view);

        element.Bind(Layoutable.WidthProperty, new Binding("Bounds.Width") { Source = container });
        element.Bind(Layoutable.HeightProperty, new Binding("Bounds.Height") { Source = container });

        container.Children.Add(element);
    }

    public void CloseView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var element = Unsafe.As<Control>(view);

        (element as IDisposable)?.Dispose();
        (element.DataContext as IDisposable)?.Dispose();
        element.DataContext = null;

        container.Children.Remove(element);
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

    public async Task OpenViewAsync(object view, INavigationParameter parameter)
    {
        OpenView(view);
        await PlayWithGuardAsync(view, parameter, AvaloniaNavigationAnimationPhase.Open).ConfigureAwait(true);
    }

    public async Task CloseViewAsync(object view, INavigationParameter parameter)
    {
        await PlayWithGuardAsync(view, parameter, AvaloniaNavigationAnimationPhase.Close).ConfigureAwait(true);
        CloseView(view);
    }

    public async Task ActivateViewAsync(object view, object? state, INavigationParameter parameter)
    {
        ActivateView(view, state);
        await PlayWithGuardAsync(view, parameter, AvaloniaNavigationAnimationPhase.Activate).ConfigureAwait(true);
    }

    public async Task<object?> DeactivateViewAsync(object view, INavigationParameter parameter)
    {
        await PlayWithGuardAsync(view, parameter, AvaloniaNavigationAnimationPhase.Deactivate).ConfigureAwait(true);
        return DeactivateView(view);
    }

    private async Task PlayWithGuardAsync(object view, INavigationParameter parameter, AvaloniaNavigationAnimationPhase phase)
    {
        var element = Unsafe.As<Control>(view);
        element.IsHitTestVisible = false;
        try
        {
            await PlayAsync(element, parameter, phase).ConfigureAwait(true);
        }
        finally
        {
            element.IsHitTestVisible = true;
        }
    }

    private Task PlayAsync(Control element, INavigationParameter parameter, AvaloniaNavigationAnimationPhase phase)
    {
        var key = parameter.AnimationKind;
        if (key is null || !options.Animations.TryGetValue(key, out var animation))
        {
            return Task.CompletedTask;
        }

        var container = resolver.Container;
        if (container is null)
        {
            return Task.CompletedTask;
        }

        return animation.PlayAsync(new AvaloniaNavigationAnimationContext
        {
            Container = container,
            View = element,
            Phase = phase,
            Parameter = parameter,
        });
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
