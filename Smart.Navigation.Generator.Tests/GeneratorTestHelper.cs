namespace Smart.Navigation.Generator.Tests;

using System.Collections.Generic;

using Microsoft.CodeAnalysis;

using Smart.Navigation.Attributes;

using SourceGenerateHelper.Testing;

internal static class GeneratorTestHelper
{
    private static GeneratorTestRunner Runner => GeneratorTestRunner
        .For<NavigationGenerator>()
        .WithReference(typeof(ViewSourceAttribute).Assembly)
        .WithDiagnosticPrefix("SNV");

    public static IReadOnlyList<Diagnostic> GetDiagnostics(string source) => Runner.GetDiagnostics(source);

    public static IReadOnlyList<Diagnostic> GetDiagnosticsAll(string source) => Runner.GetDiagnosticsAll(source);

    public static string GetGeneratedSource(string source) => Runner.GetGeneratedSource(source);
}
