namespace Smart.Navigation.Generator.Models;

using Microsoft.CodeAnalysis;

internal sealed record SourceModel(
    string Namespace,
    string ClassName,
    bool IsValueType,
    Accessibility MethodAccessibility,
    string MethodName,
    string ReturnTypeName,
    string EntryTypeName,
    string ViewIdClassFullName);
