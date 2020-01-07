namespace Example.FormsApp
{
    using Smart.ComponentModel;
    using Smart.Forms.ViewModels;

    public class ApplicationState : NotificationObject, IBusyState
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
    }
}
