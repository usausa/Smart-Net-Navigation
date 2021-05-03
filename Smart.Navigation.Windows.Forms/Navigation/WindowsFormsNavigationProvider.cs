namespace Smart.Navigation
{
    using System.Windows.Forms;

    public sealed class WindowsFormsNavigationProvider : INavigationProvider
    {
        private readonly Control container;

        private readonly WindowsFormsNavigationProviderOptions options;

        public WindowsFormsNavigationProvider(Control container, WindowsFormsNavigationProviderOptions options)
        {
            this.container = container;
            this.options = options;
        }

        public object ResolveTarget(object view)
        {
            return view;
        }

        public void OpenView(object view)
        {
            var control = (Control)view;

            if (options.FitToParent)
            {
                control.Size = container.Size;
            }

            container.Controls.Add(control);
            if (options.FitToParent)
            {
                control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        public void CloseView(object view)
        {
            var control = (Control)view;

            control.Visible = false;
            container.Controls.Remove(control);

            control.Dispose();
        }

        public void ActivateView(object view, object? parameter)
        {
            var control = (Control)view;

            control.Visible = true;

            if (options.RestoreFocus)
            {
                if (parameter is Control focused)
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
            var control = (Control)view;

            var parameter = options.RestoreFocus ? GetFocused(control) : null;

            control.Visible = false;

            return parameter;
        }

        private static Control GetFocused(Control control)
        {
            var containerControl = control as IContainerControl;
            while (containerControl is not null)
            {
                control = containerControl.ActiveControl;
                containerControl = control as IContainerControl;
            }

            return control;
        }
    }
}
