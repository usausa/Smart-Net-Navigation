namespace Smart.Navigation;

using Microsoft.Maui.Layouts;

public sealed class MauiNavigationProvider : INavigationProvider
{
    private readonly IContainerResolver resolver;

    private readonly MauiNavigationProviderOptions options;

    public MauiNavigationProvider(IContainerResolver resolver, MauiNavigationProviderOptions options)
    {
        this.resolver = resolver;
        this.options = options;
    }

    public object ResolveTarget(object view)
    {
        return ((View)view).BindingContext;
    }

    public void OpenView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var v = (View)view;

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

        var v = (View)view;

        Cleanup(v);
        (view as IDisposable)?.Dispose();
        (v.BindingContext as IDisposable)?.Dispose();
        v.BindingContext = null;

        container.Children.Remove(v);

        Disconnect(v);
    }

    public void ActivateView(object view, object? parameter)
    {
        var v = (View)view;

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
        var v = (View)view;

        var parameter = options.RestoreFocus ? GetFocused(v) : null;

        v.IsVisible = false;

        return parameter;
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
