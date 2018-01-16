namespace Smart.Mock
{
    public abstract class MockView
    {
        public object Context { get; set; }

        public bool IsVisible { get; set; }

        public object Focused { get; set; }
    }
}
