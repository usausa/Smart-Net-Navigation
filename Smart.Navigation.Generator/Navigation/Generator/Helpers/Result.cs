namespace Smart.Navigation.Generator.Helpers;

public sealed record Result<TValue>(TValue Value, DiagnosticInfo? Error)
    where TValue : IEquatable<TValue>;

public static class Results
{
    public static Result<TValue> Success<TValue>(TValue value)
        where TValue : IEquatable<TValue>
        => new(value, null);

    public static Result<TValue> Error<TValue>(DiagnosticInfo? error)
        where TValue : IEquatable<TValue>
        => new(default!, error);
}
