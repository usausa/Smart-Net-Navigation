namespace Smart.Navigation.Generator.Helpers;

public readonly struct EquatableArray<T> : IEquatable<EquatableArray<T>>
{
    private readonly T[] array;

    public EquatableArray(T[] array)
    {
        this.array = array;
    }

    public T[] ToArray() => array;

    public bool Equals(EquatableArray<T> other) => array.SequenceEqual(other.array);

    public override bool Equals(object? obj) => obj is EquatableArray<T> other && Equals(other);

    public override int GetHashCode()
    {
        var hash = 0;
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in array)
        {
            hash ^= item?.GetHashCode() ?? 0;
        }
        return hash;
    }

    public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right) => left.Equals(right);

    public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right) => !left.Equals(right);
}
