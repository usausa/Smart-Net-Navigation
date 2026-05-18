namespace Smart.Navigation;

using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

public sealed class WindowsNavigationProvider : INavigationProvider, IAsyncNavigationProvider
{
    private readonly IContainerResolver resolver;

    private readonly WindowsNavigationProviderOptions options;

    public WindowsNavigationProvider(IContainerResolver resolver, WindowsNavigationProviderOptions options)
    {
        this.resolver = resolver;
        this.options = options;
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

        var element = Unsafe.As<FrameworkElement>(view);

        if (options.FitToParent)
        {
            BindingOperations.SetBinding(
                element,
                FrameworkElement.HeightProperty,
                new Binding { Source = container, Path = new PropertyPath("ActualHeight") });
            BindingOperations.SetBinding(
                element,
                FrameworkElement.WidthProperty,
                new Binding { Source = container, Path = new PropertyPath("ActualWidth") });
        }

        container.Children.Add(element);
    }

    public void CloseView(object view)
    {
        var container = resolver.Container;
        if (container is null)
        {
            throw new InvalidOperationException("Container is unresolved.");
        }

        var element = Unsafe.As<FrameworkElement>(view);

        (element as IDisposable)?.Dispose();
        (element.DataContext as IDisposable)?.Dispose();
        element.DataContext = null;

        container.Children.Remove(element);
    }

    public void ActivateView(object view, object? parameter)
    {
        var element = Unsafe.As<FrameworkElement>(view);

        element.Visibility = Visibility.Visible;

        var control = Unsafe.As<Control>(view);
        if (options.RestoreFocus)
        {
            if (parameter is IInputElement focused)
            {
                focused.Focus();
            }
            else
            {
                if (!control.Focus())
                {
                    var fs = FocusManager.GetFocusScope(control);
                    FocusManager.SetFocusedElement(fs, control);
                }
            }
        }
    }

    public object? DeactivateView(object view)
    {
        var element = Unsafe.As<FrameworkElement>(view);

        var parameter = options.RestoreFocus ? FocusManager.GetFocusedElement(element) : null;

        element.Visibility = Visibility.Hidden;

        return parameter;
    }

    public async Task OpenViewAsync(object view, INavigationParameter parameter)
    {
        OpenView(view);
        await PlayAsync(view, parameter, WindowsNavigationAnimationPhase.Open).ConfigureAwait(true);
    }

    public async Task CloseViewAsync(object view, INavigationParameter parameter)
    {
        await PlayAsync(view, parameter, WindowsNavigationAnimationPhase.Close).ConfigureAwait(true);
        CloseView(view);
    }

    public async Task ActivateViewAsync(object view, object? state, INavigationParameter parameter)
    {
        ActivateView(view, state);
        await PlayAsync(view, parameter, WindowsNavigationAnimationPhase.Activate).ConfigureAwait(true);
    }

    public async Task<object?> DeactivateViewAsync(object view, INavigationParameter parameter)
    {
        await PlayAsync(view, parameter, WindowsNavigationAnimationPhase.Deactivate).ConfigureAwait(true);
        return DeactivateView(view);
    }

    private Task PlayAsync(object view, INavigationParameter parameter, WindowsNavigationAnimationPhase phase)
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

        return animation.PlayAsync(new WindowsNavigationAnimationContext
        {
            Container = container,
            View = Unsafe.As<FrameworkElement>(view),
            Phase = phase,
            Parameter = parameter,
        });
    }
}
