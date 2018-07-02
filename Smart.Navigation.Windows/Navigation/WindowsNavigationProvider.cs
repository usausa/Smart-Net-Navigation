namespace Smart.Navigation
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
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

            container.Children.Add(element);
            element.Height = container.ActualHeight;
            element.Width = container.ActualWidth;
        }

        public void CloseView(object view)
        {
            (view as IDisposable)?.Dispose();
            (ResolveTarget(view) as IDisposable)?.Dispose();

            var container = resolver.Container;
            if (container == null)
            {
                throw new InvalidOperationException("Container is unresolved.");
            }

            var element = (FrameworkElement)view;
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