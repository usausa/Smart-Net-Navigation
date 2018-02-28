namespace Example.FormsApp.Modules.Debug
{
    using System;
    using System.Threading.Tasks;

    using Smart.Forms.Input;
    using Smart.Navigation;

    public class DebugViewModel : AppViewModelBase
    {
        public AsyncCommand<ViewId> Forward { get; }

        public AsyncCommand Delay { get; }

        public DelegateCommand Collect { get; }

        public DebugViewModel(ApplicationState applicationState)
            : base(applicationState)
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
            Delay = MakeAsyncCommand(ExecuteDelay);
            Collect = MakeDelegateCommand(ExecuteCollect);
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
