namespace Smart.Navigation
{
    using Xamarin.Forms;

    public class FormsNavigationProvider : INavigationProvider
    {
        public object ResolveTarget(object view)
        {
            var page = (ContentView)view;
            return page.BindingContext;
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
