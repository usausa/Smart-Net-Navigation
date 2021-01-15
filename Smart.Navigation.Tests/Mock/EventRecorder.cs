namespace Smart.Mock
{
    using System.Collections.Generic;

    public class EventRecorder
    {
        public IList<string> Events { get; } = new List<string>();
    }
}
