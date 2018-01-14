namespace Smart.Navigation
{
    using System.Windows.Forms;

    public sealed class ControlPageProvider : INavigationProvider
    {
        private readonly Control container;

        public bool FitToParent { get; set; } = true;

        public bool RestoreFocus { get; set; } = true;

        public ControlPageProvider(Control container)
        {
            this.container = container;
        }

        public object ResolveTarget(object page)
        {
            return page;
        }

        public void OpenPage(object page)
        {
            var control = (Control)page;

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

        public void ClosePage(object page)
        {
            var control = (Control)page;

            container.Controls.Remove(control);
            control.Parent = null;

            control.Dispose();
        }

        public void ActivePage(object page, object parameter)
        {
            var control = (Control)page;

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

        public object DectivePage(object page)
        {
            var control = (Control)page;

            var parameter = default(object);

            if (RestoreFocus)
            {
                while (control.Parent != null)
                {
                    control = control.Parent;
                }

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
