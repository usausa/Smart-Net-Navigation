namespace Example.FormsApp.Modules.Debug
{
    using Smart.Forms.Input;
    using Smart.Navigation;

    public class DebugLeakViewModel : INavigatorAware
    {
        public INavigator Navigator { get; set; }

        public AsyncCommand<ViewId> Forward { get; }

        public DebugLeakViewModel()
        {
            Forward = new AsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }

        ~DebugLeakViewModel()
        {
            System.Diagnostics.Debug.WriteLine("~DebugLeakViewModel()");
        }
    }
}
