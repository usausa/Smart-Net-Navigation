namespace Smart.Navigation.Generator.Models;

using SourceGenerateHelper;

internal sealed record ViewSourceModel(
    SourceModel Source,
    EquatableArray<ViewIdModel> Views);
