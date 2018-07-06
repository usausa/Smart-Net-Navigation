namespace Smart.Navigation
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public class WindowsNavigationProvider : INavigationProvider
    {
        private readonly IContainerResolver resolver;

        private readonly WindowsNavigationProviderOptions options;

        public WindowsNavigationProvider(IContainerResolver resolver, WindowsNavigationProviderOptions options)
        {
            this.resolver = resolver;
            this.options = options;
        }

        public object ResolveTarget(object view)
        {
            return ((FrameworkElement)view).DataContext;
        }

        public void OpenView(object view)
        {
            var container = resolver.Container;
            if (container == null)
            {
                throw new InvalidOperationException("Container is unresolved.");
            }

            var element = (FrameworkElement)view;

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
            if (container == null)
            {
                throw new InvalidOperationException("Container is unresolved.");
            }

            var element = (FrameworkElement)view;

            (view as IDisposable)?.Dispose();
            (element.DataContext as IDisposable)?.Dispose();

            container.Children.Remove(element);
        }

        public void ActiveView(object view, object parameter)
        {
            var element = (FrameworkElement)view;

            element.Visibility = Visibility.Visible;

            var control = (Control)view;
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

        public object DeactiveView(object view)
        {
            var element = (FrameworkElement)view;

            var parameter = options.RestoreFocus ? FocusManager.GetFocusedElement(element) : null;

            element.Visibility = Visibility.Hidden;

            return parameter;
        }
    }
}