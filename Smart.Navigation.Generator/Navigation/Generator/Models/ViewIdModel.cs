namespace Smart.Navigation.Generator.Models;

internal sealed record ViewIdModel(
    string ClassFullName,
    string ViewIdClassFullName,
    string ViewIdFullName,
    object? Value);
