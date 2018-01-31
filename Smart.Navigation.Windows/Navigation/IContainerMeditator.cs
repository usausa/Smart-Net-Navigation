namespace Smart.Navigation
{
    public interface IContainerMeditator
    {
        void OpenView(object container, object view);

        void CloseView(object container, object view);

        void ActiveView(object container, object view, object parameter);

        object DeactiveView(object container, object view);
    }
}
