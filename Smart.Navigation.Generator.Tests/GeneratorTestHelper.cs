namespace Smart.Navigation.Generator.Tests;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Smart.Navigation.Attributes;

internal static class GeneratorTestHelper
{
    private static readonly Assembly SmartNavigationAssembly = typeof(ViewSourceAttribute).Assembly;

    private static readonly Lazy<bool> EnsureDeps = new(() =>
    {
        var dir = Path.GetDirectoryName(typeof(GeneratorTestHelper).Assembly.Location)!;
        var helper = Path.Combine(dir, "SourceGenerateHelper.dll");
        if (File.Exists(helper))
        {
            Assembly.LoadFrom(helper);
        }

        return true;
    });

    public static IReadOnlyList<Diagnostic> GetDiagnostics(string source) =>
        RunGenerator(source).Diagnostics
            .Where(d => d.Id.StartsWith("SNV", StringComparison.Ordinal))
            .ToList();

    public static IReadOnlyList<Diagnostic> GetDiagnosticsAll(string source) =>
        RunGenerator(source).Diagnostics.ToList();

    public static string GetGeneratedSource(string source) =>
        RunGenerator(source).GeneratedSource;

    private static (IEnumerable<Diagnostic> Diagnostics, string GeneratedSource) RunGenerator(string source)
    {
        _ = EnsureDeps.Value;

        var parseOptions = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview);
        var syntaxTree = CSharpSyntaxTree.ParseText(source, parseOptions);

        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(List<>).Assembly.Location),
            MetadataReference.CreateFromFile(SmartNavigationAssembly.Location)
        }.Concat(GetRuntimeReferences());

        var compilation = CSharpCompilation.Create(
            assemblyName: "TestAssembly",
            syntaxTrees: [syntaxTree],
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var generator = new NavigationGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            generators: [generator.AsSourceGenerator()],
            parseOptions: parseOptions);

        driver = driver.RunGeneratorsAndUpdateCompilation(
            compilation, out var outputCompilation, out var generatorDiagnostics);

        var driverResult = driver.GetRunResult();

        var diagnostics = driverResult.Results
            .SelectMany(r => r.Diagnostics)
            .Concat(generatorDiagnostics)
            .Concat(outputCompilation.GetDiagnostics());

        var generatedSource = driverResult.Results
            .SelectMany(r => r.GeneratedSources)
            .Select(s => s.SourceText.ToString())
            .FirstOrDefault() ?? string.Empty;

        return (diagnostics, generatedSource);
    }

    private static IEnumerable<MetadataReference> GetRuntimeReferences()
    {
        if (AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") is not string trustedAssemblies)
        {
            yield break;
        }

        foreach (var path in trustedAssemblies.Split(Path.PathSeparator))
        {
            if (!String.IsNullOrEmpty(path))
            {
                yield return MetadataReference.CreateFromFile(path);
            }
        }
    }
}
