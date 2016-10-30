namespace Smart.Navigation.Mocks
{
    using Smart.ComponentModel;

    public class MockViewModelBase : DisposableObject, INavigatorAware, INavigationEventSupport, INotifySupport
    {
        public INavigator Navigator { get; set; }

        public virtual void OnNavigatedFrom(INavigationContext context)
        {
        }

        public virtual void OnNavigatedTo(INavigationContext context)
        {
        }

        public virtual void NavigatorNotify(object parameter)
        {
        }
    }
}