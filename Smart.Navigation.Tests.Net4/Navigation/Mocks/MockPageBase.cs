namespace Smart.Navigation.Mocks
{
    using Smart.ComponentModel;

    public class MockPageBase : DisposableObject, INavigatorAware, INavigationEventSupport, IConfirmRequest, INotifySupport
    {
        public INavigator Navigator { get; set; }

        public object Focused { get; set; }

        public bool IsOpen { get; private set; }

        public bool IsActive { get; private set; }

        public virtual void Open()
        {
            IsOpen = true;
            IsActive = true;
        }

        public virtual void Close()
        {
            IsOpen = false;
            IsActive = false;
        }

        public virtual void Active()
        {
            IsActive = true;
        }

        public virtual void Deactive()
        {
            IsActive = false;
        }

        public virtual void OnNavigatedFrom(INavigationContext context)
        {
        }

        public virtual void OnNavigatedTo(INavigationContext context)
        {
        }

        public virtual void NavigationConfirm(INavigationContext context, ConfirmOperation operation)
        {
        }

        public virtual void NavigatorNotify(object parameter)
        {
        }
    }
}
