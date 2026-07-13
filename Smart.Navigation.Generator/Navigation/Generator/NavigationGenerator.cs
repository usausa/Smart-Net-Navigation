namespace Smart.Navigation.Generator;

using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using Smart.Navigation.Generator.Models;

using SourceGenerateHelper;

[Generator]
public sealed class NavigationGenerator : IIncrementalGenerator
{
    private const string ViewSourceAttributeName = "Smart.Navigation.Attributes.ViewSourceAttribute";
    private const string ViewAttributeName = "Smart.Navigation.Attributes.ViewAttribute";

    private const string EnumerableName = "System.Collections.Generic.IEnumerable<T>";
    private const string KeyValuePairName = "System.Collections.Generic.KeyValuePair<TKey, TValue>";
    private const string TypeName = "System.Type";

    // ------------------------------------------------------------
    // Initialize
    // ------------------------------------------------------------

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var sourceProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                ViewSourceAttributeName,
                static (node, _) => IsSourceTargetSyntax(node),
                static (context, _) => GetSourceModel(context))
            .Where(static x => x is not null)
            .Collect();

        var viewProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                ViewAttributeName,
                static (node, _) => IsViewIdTargetSyntax(node),
                static (context, _) => GetViewIdModel(context))
            .Where(static x => x is not null)
            .Collect();

        context.RegisterSourceOutput(sourceProvider, static (context, sources) => ReportDiagnostics(context, sources));
        context.RegisterSourceOutput(viewProvider, static (context, views) => ReportDiagnostics(context, views));

        var models = sourceProvider
            .Combine(viewProvider)
            .SelectMany(static (pair, token) => JoinSourcesWithViews(pair.Left, pair.Right, token));

        context.RegisterImplementationSourceOutput(models, static (context, model) => Execute(context, model));
    }

    // ------------------------------------------------------------
    // Parser
    // ------------------------------------------------------------

    private static bool IsSourceTargetSyntax(SyntaxNode node) =>
        node is MethodDeclarationSyntax;

    private static Result<SourceModel> GetSourceModel(GeneratorAttributeSyntaxContext context)
    {
        var syntax = context.TargetNode;
        var methodSymbol = (IMethodSymbol)context.TargetSymbol;

        // Validate method style
        if (!methodSymbol.IsStatic || !methodSymbol.IsPartialDefinition)
        {
            return Results.Error<SourceModel>(new DiagnosticInfo(Diagnostics.InvalidMethodDefinition, syntax.GetLocation(), methodSymbol.Name));
        }

        // Validate argument
        if (methodSymbol.Parameters.Length != 0)
        {
            return Results.Error<SourceModel>(new DiagnosticInfo(Diagnostics.InvalidMethodParameter, syntax.GetLocation(), methodSymbol.Name));
        }

        // Validate return type
        if ((methodSymbol.ReturnType is not INamedTypeSymbol returnTypeSymbol) ||
            (returnTypeSymbol.ConstructedFrom.ToDisplayString() != EnumerableName) ||
            (returnTypeSymbol.TypeArguments[0] is not INamedTypeSymbol keyValueTypeSymbol) ||
            (keyValueTypeSymbol.ConstructedFrom.ToDisplayString() != KeyValuePairName) ||
            (keyValueTypeSymbol.TypeArguments[1].ToDisplayString() != TypeName))
        {
            return Results.Error<SourceModel>(new DiagnosticInfo(Diagnostics.InvalidMethodReturnType, syntax.GetLocation(), methodSymbol.Name));
        }

        var containingType = methodSymbol.ContainingType;
        var ns = String.IsNullOrEmpty(containingType.ContainingNamespace.Name)
            ? string.Empty
            : containingType.ContainingNamespace.ToDisplayString();

        return Results.Success(new SourceModel(
            ns,
            containingType.GetClassName(),
            containingType.IsValueType,
            methodSymbol.DeclaredAccessibility,
            methodSymbol.Name,
            returnTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            keyValueTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            keyValueTypeSymbol.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)));
    }

    private static bool IsViewIdTargetSyntax(SyntaxNode node) =>
        node is ClassDeclarationSyntax;

    private static Result<EquatableArray<ViewIdModel>> GetViewIdModel(GeneratorAttributeSyntaxContext context)
    {
        var classSymbol = (ITypeSymbol)context.TargetSymbol;

        return Results.Success(new EquatableArray<ViewIdModel>(
            classSymbol.GetAttributes()
                .Where(static x => x.AttributeClass?.ToDisplayString() == ViewAttributeName)
                .Select(attribute => new ViewIdModel(
                    classSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    attribute.ConstructorArguments[0].Type!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    attribute.ConstructorArguments[0].ToCSharpString(),
                    attribute.ConstructorArguments[0].Value!.ToString()))
                .ToArray()));
    }

    private static ImmutableArray<ViewSourceModel> JoinSourcesWithViews(
        ImmutableArray<Result<SourceModel>> sourceResults,
        ImmutableArray<Result<EquatableArray<ViewIdModel>>> viewResults,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var viewMap = viewResults
            .SelectValue()
            .SelectMany(static x => x)
            .GroupBy(static x => x.ViewIdClassFullName)
            .ToDictionary(static x => x.Key, static x => x.ToArray());

        token.ThrowIfCancellationRequested();

        var builder = ImmutableArray.CreateBuilder<ViewSourceModel>();
        foreach (var source in sourceResults.SelectValue())
        {
            if (viewMap.TryGetValue(source.ViewIdClassFullName, out var views))
            {
                builder.Add(new ViewSourceModel(source, new EquatableArray<ViewIdModel>(views)));
            }
        }

        return builder.ToImmutable();
    }

    // ------------------------------------------------------------
    // Generator
    // ------------------------------------------------------------

    private static void ReportDiagnostics<T>(SourceProductionContext context, ImmutableArray<Result<T>> results)
        where T : IEquatable<T>
    {
        foreach (var info in results.SelectError())
        {
            context.ReportDiagnostic(info);
        }
    }

    private static void Execute(SourceProductionContext context, ViewSourceModel model)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        var builder = new SourceBuilder();
        BuildSource(builder, model.Source, model.Views);

        var filename = MakeFilename(model.Source.Namespace, model.Source.ClassName, model.Source.MethodName);
        context.AddSource(filename, SourceText.From(builder.ToString(), Encoding.UTF8));
    }

    private static void BuildSource(SourceBuilder builder, SourceModel source, IEnumerable<ViewIdModel> viewIds)
    {
        builder.AutoGenerated();
        builder.EnableNullable();
        builder.NewLine();

        // namespace
        if (!String.IsNullOrEmpty(source.Namespace))
        {
            builder.Namespace(source.Namespace);
            builder.NewLine();
        }

        // class
        builder
            .Indent()
            .Append("partial ")
            .Append(source.IsValueType ? "struct " : "class ")
            .Append(source.ClassName)
            .NewLine();
        builder.BeginScope();

        // method
        builder
            .Indent()
            .Append(source.MethodAccessibility.ToText())
            .Append(" static partial ")
            .Append(source.ReturnTypeName)
            .Append(' ')
            .Append(source.MethodName)
            .Append("()")
            .NewLine();
        builder.BeginScope();

        foreach (var viewId in viewIds)
        {
            builder
                .Indent()
                .Append("yield return new ")
                .Append(source.EntryTypeName)
                .Append('(')
                .Append(viewId.ViewIdFullName)
                .Append(", typeof(")
                .Append(viewId.ClassFullName)
                .Append("));")
                .NewLine();
        }

        builder.EndScope();

        builder.EndScope();
    }

    // ------------------------------------------------------------
    // Helper
    // ------------------------------------------------------------

    private static string MakeFilename(string ns, string className, string methodName)
    {
        var buffer = new StringBuilder();

        if (!String.IsNullOrEmpty(ns))
        {
            buffer.Append(ns.Replace('.', '_'));
            buffer.Append('_');
        }

        buffer.Append(className.Replace('<', '[').Replace('>', ']'));
        buffer.Append('_');
        buffer.Append(methodName);
        buffer.Append(".g.cs");

        return buffer.ToString();
    }
}
