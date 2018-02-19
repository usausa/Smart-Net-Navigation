namespace Example.FormsApp.Views
{
    using System;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class MenuViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> Forward { get; }

        public DelegateCommand Collect { get; }

        public MenuViewModel()
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
            Collect = MakeDelegateCommand(ExecuteCollect);
        }

        private void ExecuteCollect()
        {
            GC.Collect();
        }
    }
}
