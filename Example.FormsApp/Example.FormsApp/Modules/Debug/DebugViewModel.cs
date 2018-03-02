namespace Example.FormsApp.Modules.Debug
{
    using System;
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class DebugViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> ForwardCommand { get; }

        public AsyncCommand DelayCommand { get; }

        public DelegateCommand CollectCommand { get; }

        public DebugViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
            DelayCommand = MakeAsyncCommand(ExecuteDelay);
            CollectCommand = MakeDelegateCommand(ExecuteCollect);
        }

        private Task ExecuteDelay()
        {
            return Task.Delay(3000);
        }

        private void ExecuteCollect()
        {
            // TODO async ?
#pragma warning disable S1215 // "GC.Collect" should not be called
            GC.Collect();
#pragma warning restore S1215 // "GC.Collect" should not be called
        }
    }
}
