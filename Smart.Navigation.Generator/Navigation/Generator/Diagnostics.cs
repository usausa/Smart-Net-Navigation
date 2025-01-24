namespace Smart.Navigation.Generator;

using Microsoft.CodeAnalysis;

internal static class Diagnostics
{
    public static DiagnosticDescriptor InvalidMethodDefinition => new(
        id: "SNV0001",
        title: "Invalid method definition",
        messageFormat: "Method must be partial extension. method=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor InvalidMethodParameter => new(
        id: "SNV0002",
        title: "Invalid method parameter",
        messageFormat: "Parameter count must be nothing. method=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor InvalidMethodReturnType => new(
        id: "SNV0003",
        title: "Invalid method return type",
        messageFormat: "Return type must be IEnumerable<KeyValuePair<ViewId, Type>>. method=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
}
