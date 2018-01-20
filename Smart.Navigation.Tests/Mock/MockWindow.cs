namespace Smart.Mock
{
    public abstract class MockWindow
    {
        public object Context { get; set; }

        public bool IsVisible { get; set; }

        public object Focused { get; set; }
    }
}
