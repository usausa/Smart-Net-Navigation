namespace Smart.Navigation.Components
{
    using System;
    using System.Threading;

    public sealed class StandardConverter : IConverter
    {
        public object Convert(object value, Type type)
        {
            return System.Convert.ChangeType(value, type, Thread.CurrentThread.CurrentCulture);
        }
    }
}
