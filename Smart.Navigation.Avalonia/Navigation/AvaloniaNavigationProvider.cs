namespace Smart.Navigation;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;

public sealed class AvaloniaNavigationProvider : INavigationProvider
{
    private readonly IContainerResolver resolver;

    private readonly AvaloniaNavigationProviderOptions options;

    public AvaloniaNavigationProvider(IContainerResolver resolver, AvaloniaNavigationProviderOptions options)
    {
        this.resolver = resolver;
        this.options = options;
    }

    public object ResolveTarget(object view)
    {
        return ((Control)view).DataContext!;
    }

    public void OpenView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var element = (Control)view;

        element.Width = container.Bounds.Width;
        element.Height = container.Bounds.Height;
        container.Children.Add(element);
    }

    public void CloseView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var element = (Control)view;

        (element as IDisposable)?.Dispose();
        (element.DataContext as IDisposable)?.Dispose();
        element.DataContext = null;

        container.Children.Remove(element);
    }

    public void ActivateView(object view, object? parameter)
    {
        var element = (Control)view;

        element.IsVisible = true;

        var control = (Control)view;
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
        var element = (Control)view;

        var parameter = options.RestoreFocus ? GetFocused(element) : null;

        element.IsVisible = false;

        return parameter;
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
