namespace Smart.Navigation.Mocks
{
    using Smart.ComponentModel;

    public class MockViewBase : DisposableObject
    {
        public object Focused { get; set; }

        public bool IsOpen { get; private set; }

        public bool IsActive { get; private set; }

        public object DataContext { get; set; }

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
    }
}
