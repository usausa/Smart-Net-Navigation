namespace Smart.Navigation
{
    public interface IContainerMeditator
    {
        void OpenView(object view);

        void CloseView(object view);

        void ActiveView(object view, object parameter);

        object DeactiveView(object view);
    }
}
