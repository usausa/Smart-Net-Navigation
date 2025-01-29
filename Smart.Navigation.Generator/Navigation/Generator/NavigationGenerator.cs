namespace Smart.Navigation.Generator;

using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using Smart.Navigation.Generator.Helpers;
using Smart.Navigation.Generator.Models;

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

        context.RegisterImplementationSourceOutput(
            sourceProvider.Combine(viewProvider),
            static (context, provider) => Execute(context, provider.Left, provider.Right));
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
        var ns = string.IsNullOrEmpty(containingType.ContainingNamespace.Name)
            ? string.Empty
            : containingType.ContainingNamespace.ToDisplayString();

        return Results.Success(new SourceModel(
            ns,
            containingType.GetClassName(),
            containingType.IsValueType,
            methodSymbol.DeclaredAccessibility,
            methodSymbol.Name,
            returnTypeSymbol.ToDisplayString(),
            keyValueTypeSymbol.ToDisplayString(),
            keyValueTypeSymbol.TypeArguments[0].ToDisplayString()));
    }

    private static bool IsViewIdTargetSyntax(SyntaxNode node) =>
        node is ClassDeclarationSyntax;

    private static Result<EquatableArray<ViewIdModel>> GetViewIdModel(GeneratorAttributeSyntaxContext context)
    {
        var classSymbol = (ITypeSymbol)context.TargetSymbol;

        return Results.Success(new EquatableArray<ViewIdModel>(
            classSymbol.GetAttributes()
                .Where(static x => x.AttributeClass!.ToDisplayString() == ViewAttributeName)
                .Select(attribute => new ViewIdModel(
                    classSymbol.ToDisplayString(),
                    attribute.ConstructorArguments[0].Type!.ToDisplayString(),
                    attribute.ConstructorArguments[0].ToCSharpString(),
                    attribute.ConstructorArguments[0].Value!.ToString()))
                .ToArray()));
    }

    // ------------------------------------------------------------
    // Generator
    // ------------------------------------------------------------

    private static void Execute(SourceProductionContext context, ImmutableArray<Result<SourceModel>> viewSources, ImmutableArray<Result<EquatableArray<ViewIdModel>>> viewIds)
    {
        foreach (var info in viewSources.SelectPart(static x => x.Error))
        {
            context.ReportDiagnostic(info);
        }
        foreach (var info in viewIds.SelectPart(static x => x.Error))
        {
            context.ReportDiagnostic(info);
        }

        var viewMap = viewIds
            .SelectPart(static x => x.Value)
            .SelectMany(static x => x.ToArray())
            .GroupBy(static x => x.ViewIdClassFullName)
            .ToDictionary(static x => x.Key, static x => x.ToList());

        var builder = new SourceBuilder();
        foreach (var viewSource in viewSources.SelectPart(static x => x.Value))
        {
            context.CancellationToken.ThrowIfCancellationRequested();

            if (viewMap.TryGetValue(viewSource.ViewIdClassFullName, out var viewList))
            {
                builder.Clear();

                BuildSource(builder, viewSource, viewList);

                var filename = MakeFilename(viewSource.Namespace, viewSource.ClassName, viewSource.MethodName);
                var source = builder.ToString();
                context.AddSource(filename, SourceText.From(source, Encoding.UTF8));
            }
        }
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

        if (!string.IsNullOrEmpty(ns))
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
