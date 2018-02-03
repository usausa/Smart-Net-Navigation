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
            return ((Control)view).DataContext;
        }

        public void OpenView(object view)
        {
            var container = resolver.Container;
            if (container == null)
            {
                throw new InvalidOperationException("Container is unresolved.");
            }

            container.Content = view;
        }

        public void CloseView(object view)
        {
            (view as IDisposable)?.Dispose();
            (ResolveTarget(view) as IDisposable)?.Dispose();
        }

        public void ActiveView(object view, object parameter)
        {
            var container = resolver.Container;
            if (container == null)
            {
                throw new InvalidOperationException("Container is unresolved.");
            }

            container.Content = view;

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
            var control = (Control)view;
            return options.RestoreFocus ? FocusManager.GetFocusedElement(control) : null;
        }
    }
}