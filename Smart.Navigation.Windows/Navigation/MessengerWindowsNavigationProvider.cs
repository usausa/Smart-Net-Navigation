namespace Smart.Navigation
{
    using System.Windows.Controls;

    public class MessengerWindowsNavigationProvider : INavigationProvider
    {
        public object ResolveTarget(object view)
        {
            var control = (Control)view;
            return control.DataContext;
        }

        public void OpenView(object view)
        {
            throw new System.NotImplementedException();
        }

        public void CloseView(object view)
        {
            throw new System.NotImplementedException();
        }

        public void ActiveView(object view, object parameter)
        {
            throw new System.NotImplementedException();
        }

        public object DeactiveView(object view)
        {
            throw new System.NotImplementedException();
        }
    }
}