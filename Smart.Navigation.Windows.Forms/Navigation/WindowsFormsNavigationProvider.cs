namespace Smart.Navigation
{
    using System.Windows.Forms;

    public sealed class WindowsFormsNavigationProvider : INavigationProvider
    {
        private readonly Control container;

        public bool FitToParent { get; set; } = true;

        public bool RestoreFocus { get; set; } = true;

        public WindowsFormsNavigationProvider(Control container)
        {
            this.container = container;
        }

        public object ResolveTarget(object view)
        {
            return view;
        }

        public void OpenView(object view)
        {
            var control = (Control)view;

            if (FitToParent)
            {
                control.Size = container.Size;
            }

            container.Controls.Add(control);
            if (FitToParent)
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

        public void ActiveView(object view, object parameter)
        {
            var control = (Control)view;

            control.Visible = true;

            if (RestoreFocus)
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
            else
            {
                control.Focus();
            }
        }

        public object DeactiveView(object view)
        {
            var control = (Control)view;

            var parameter = default(object);

            if (RestoreFocus)
            {
                parameter = GetFocused(control);
            }

            control.Visible = false;

            return parameter;
        }

        private static Control GetFocused(Control control)
        {
            var containerControl = control as IContainerControl;
            while (containerControl != null)
            {
                control = containerControl.ActiveControl;
                containerControl = control as IContainerControl;
            }

            return control;
        }
    }
}
