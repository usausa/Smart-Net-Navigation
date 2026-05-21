namespace Smart.Navigation;

using System.Runtime.CompilerServices;

using Microsoft.Maui.Layouts;

public sealed class MauiNavigationProvider : INavigationProvider, IAsyncNavigationProvider
{
    private readonly IContainerResolver resolver;

    private readonly MauiNavigationProviderOptions options;

    public MauiNavigationProvider(IContainerResolver resolver, MauiNavigationProviderOptions options)
    {
        this.resolver = resolver;
        this.options = options;
    }

    public object? ResolveTarget(object view)
    {
        return Unsafe.As<View>(view).BindingContext;
    }

    public void OpenView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var v = Unsafe.As<View>(view);

        AbsoluteLayout.SetLayoutFlags(v, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.HeightProportional);
        AbsoluteLayout.SetLayoutBounds(v, new Rect(0, 0, 1, 1));
        container.Children.Add(v);
    }

    public void CloseView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var v = Unsafe.As<View>(view);

        Cleanup(v);
        (view as IDisposable)?.Dispose();
        (v.BindingContext as IDisposable)?.Dispose();
        v.BindingContext = null;

        container.Children.Remove(v);

        if (options.DisconnectHandler)
        {
            Disconnect(v);
        }
    }

    public void ActivateView(object view, object? parameter)
    {
        var v = Unsafe.As<View>(view);

        v.IsVisible = true;

        if (options.RestoreFocus)
        {
            if (parameter is VisualElement focused)
            {
                focused.Focus();
            }
            else
            {
                v.Focus();
            }
        }
    }

    public object? DeactivateView(object view)
    {
        var v = Unsafe.As<View>(view);

        var parameter = options.RestoreFocus ? GetFocused(v) : null;

        v.IsVisible = false;

        return parameter;
    }

    public async Task OpenViewAsync(object view, INavigationParameter parameter)
    {
        OpenView(view);
        await PlayWithGuardAsync(view, parameter, MauiNavigationEffectPhase.Open).ConfigureAwait(true);
    }

    public async Task CloseViewAsync(object view, INavigationParameter parameter)
    {
        await PlayWithGuardAsync(view, parameter, MauiNavigationEffectPhase.Close).ConfigureAwait(true);
        CloseView(view);
    }

    public async Task ActivateViewAsync(object view, object? state, INavigationParameter parameter)
    {
        ActivateView(view, state);
        await PlayWithGuardAsync(view, parameter, MauiNavigationEffectPhase.Activate).ConfigureAwait(true);
    }

    public async Task<object?> DeactivateViewAsync(object view, INavigationParameter parameter)
    {
        await PlayWithGuardAsync(view, parameter, MauiNavigationEffectPhase.Deactivate).ConfigureAwait(true);
        return DeactivateView(view);
    }

    private async Task PlayWithGuardAsync(object view, INavigationParameter parameter, MauiNavigationEffectPhase phase)
    {
        var element = Unsafe.As<View>(view);
        var previousInputTransparent = element.InputTransparent;
        var layout = element as Layout;
        var previousCascade = layout?.CascadeInputTransparent ?? false;
        element.InputTransparent = true;
        if (layout is not null)
        {
            layout.CascadeInputTransparent = true;
        }
        try
        {
            await PlayAsync(element, parameter, phase).ConfigureAwait(true);
        }
        finally
        {
            element.InputTransparent = previousInputTransparent;
            if (layout is not null)
            {
                layout.CascadeInputTransparent = previousCascade;
            }
        }
    }

    private Task PlayAsync(View element, INavigationParameter parameter, MauiNavigationEffectPhase phase)
    {
        var key = parameter.Effect;
        if (key is null || !options.Effects.TryGetValue(key, out var effect))
        {
            return Task.CompletedTask;
        }

        var container = resolver.Container;
        if (container is null)
        {
            return Task.CompletedTask;
        }

        return effect.PlayAsync(new MauiNavigationEffectContext
        {
            Container = container,
            View = element,
            Phase = phase,
            Parameter = parameter,
        });
    }

    private static void Cleanup(IVisualTreeElement parent)
    {
        if (parent is VisualElement visualElement)
        {
            visualElement.Behaviors.Clear();
            visualElement.Triggers.Clear();
        }

        foreach (var child in parent.GetVisualChildren())
        {
            Cleanup(child);
        }
    }

    private static void Disconnect(IVisualTreeElement parent)
    {
        if (parent is VisualElement visualElement)
        {
            visualElement.Handler?.DisconnectHandler();
        }

        foreach (var child in parent.GetVisualChildren())
        {
            Disconnect(child);
        }
    }

    private static VisualElement? GetFocused(IVisualTreeElement parent)
    {
        if (parent is VisualElement { IsFocused: true } visualElement)
        {
            return visualElement;
        }

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
