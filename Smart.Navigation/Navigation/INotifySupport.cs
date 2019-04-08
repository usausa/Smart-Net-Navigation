namespace Smart.Navigation
{
    public interface INotifySupport
    {
        void NavigatorNotify(object parameter);
    }

    public interface INotifySupport<in T>
    {
        void NavigatorNotify(T parameter);
    }
}
