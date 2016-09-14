namespace Smart.Navigation.Mocks
{
    using Smart.ComponentModel;
    using Smart.Navigation.Plugins.Scope;

    public class MockContextBase : DisposableObject, IScopeLifecycle
    {
        public bool Active { get; private set; }

        public virtual void Initialize()
        {
            Active = true;
        }

        protected override void Dispose(bool disposing)
        {
            Active = false;

            base.Dispose(disposing);
        }
    }
}
