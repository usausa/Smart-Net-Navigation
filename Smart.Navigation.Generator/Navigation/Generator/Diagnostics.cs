namespace Smart.Navigation.Generator;

using Microsoft.CodeAnalysis;

using SourceGenerateHelper;

internal static class Diagnostics
{
    public static DiagnosticDescriptor InvalidMethodDefinition { get; } = new(
        id: "SNV0001",
        title: "Invalid method definition",
        messageFormat: "[ViewSource] method must be static partial without an implementation. method=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        customTags: DiagnosticTags.NotSuppressible);

    public static DiagnosticDescriptor InvalidMethodParameter { get; } = new(
        id: "SNV0002",
        title: "Invalid method parameter",
        messageFormat: "[ViewSource] method must not have parameters. method=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        customTags: DiagnosticTags.NotSuppressible);

    public static DiagnosticDescriptor InvalidMethodReturnType { get; } = new(
        id: "SNV0003",
        title: "Invalid method return type",
        messageFormat: "[ViewSource] return type must be IEnumerable<KeyValuePair<ViewId, Type>>. method=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        customTags: DiagnosticTags.NotSuppressible);

    public static DiagnosticDescriptor InvalidViewClass { get; } = new(
        id: "SNV0004",
        title: "View class cannot be referred to",
        messageFormat: "[View] class must not be file-local or nested in a private or protected type, and is not registered. class=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        customTags: DiagnosticTags.NotSuppressible);

    public static DiagnosticDescriptor HintNameCollision { get; } = new(
        id: "SNV0005",
        title: "Name differs only in case",
        messageFormat: "[ViewSource] method or type name differs only in case from another one, and its source is not generated. method=[{0}], other=[{1}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        customTags: DiagnosticTags.NotSuppressible);
}
