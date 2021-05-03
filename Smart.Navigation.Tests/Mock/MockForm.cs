namespace Smart.Mock
{
    using System;

    public abstract class MockForm : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public bool IsOpen { get; private set; }

        public bool IsVisible { get; set; }

        public object? Focused { get; set; }

        ~MockForm()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                IsDisposed = true;

                IsOpen = false;
                IsVisible = false;
            }
        }

        public void Show()
        {
            IsOpen = true;
            IsVisible = true;
            Focused = this;
        }
    }
}
