namespace Smart.Mock
{
    using System.Diagnostics.CodeAnalysis;

    public class Holder<T>
    {
        [AllowNull]
        public T Value { get; set; }
    }
}
