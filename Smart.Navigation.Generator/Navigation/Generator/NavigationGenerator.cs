namespace Smart.Navigation.Generator;

using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Smart.Navigation.Generator.Models;

using SourceGenerateHelper;

[Generator]
public sealed class NavigationGenerator : IIncrementalGenerator
{
    private const string ViewSourceAttributeName = "Smart.Navigation.Attributes.ViewSourceAttribute";
    private const string ViewAttributeName = "Smart.Navigation.Attributes.ViewAttribute";

    private const string EnumerableName = "System.Collections.Generic.IEnumerable`1";
    private const string KeyValuePairName = "System.Collections.Generic.KeyValuePair`2";
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
        var sourceTreeProvider = context.ForAttributeWithMetadataNameSyntaxTrees(
            ViewSourceAttributeName,
            static (node, _) => IsSourceTargetSyntax(node));

        var viewProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                ViewAttributeName,
                static (node, _) => IsViewIdTargetSyntax(node),
                static (context, _) => GetViewIdModel(context))
            .Where(static x => x is not null)
            .Collect();
        var viewTreeProvider = context.ForAttributeWithMetadataNameSyntaxTrees(
            ViewAttributeName,
            static (node, _) => IsViewIdTargetSyntax(node));

        context.RegisterSourceOutput(
            sourceProvider.Combine(sourceTreeProvider),
            static (context, pair) => context.ReportDiagnostics(pair.Left.SelectError().Concat(FindHintNameCollisions(pair.Left).Values).Distinct(), pair.Right));
        context.RegisterSourceOutput(
            viewProvider.Combine(viewTreeProvider),
            static (context, pair) => context.ReportDiagnostics(pair.Left.SelectError().Distinct(), pair.Right));

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
        var syntax = (MethodDeclarationSyntax)context.TargetNode;
        var methodSymbol = (IMethodSymbol)context.TargetSymbol;

        // Validate method style
        if (!methodSymbol.IsStatic || !methodSymbol.IsPartialDefinition || (methodSymbol.PartialImplementationPart is not null))
        {
            return Results.Error<SourceModel>(new DiagnosticInfo(Diagnostics.InvalidMethodDefinition, syntax.Identifier.GetLocation(), methodSymbol.Name));
        }

        var containingType = methodSymbol.ContainingType;
        var ns = String.IsNullOrEmpty(containingType.ContainingNamespace.Name)
            ? string.Empty
            : containingType.ContainingNamespace.ToDisplayString();
        var containingTypes = new EquatableArray<string>(containingType.GetContainingTypes()
            .Append(containingType)
            .Select(static x => x.GetPartialDeclaration())
            .ToArray());
        var hintName = HintNameBuilder.BuildFromType(containingType, methodSymbol.Name);
        var signature = methodSymbol.GetImplementationSignature(syntax);

        var methodName = $"{containingType.ToDisplayString()}.{methodSymbol.Name}";

        Result<SourceModel> Fallback(DiagnosticInfo error) =>
            new(
                new SourceModel(ns, containingTypes, hintName, signature, string.Empty, string.Empty, IsFallback: true, MethodName: methodName),
                new EquatableArray<DiagnosticInfo>([error]));

        // Validate argument
        if (methodSymbol.Parameters.Length != 0)
        {
            return Fallback(new DiagnosticInfo(Diagnostics.InvalidMethodParameter, syntax.Identifier.GetLocation(), methodSymbol.Name));
        }

        // Validate return type
        if ((methodSymbol.ReturnType is not INamedTypeSymbol returnTypeSymbol) ||
            !returnTypeSymbol.HasFullyQualifiedMetadataName(EnumerableName) ||
            (returnTypeSymbol.TypeArguments[0] is not INamedTypeSymbol keyValueTypeSymbol) ||
            !keyValueTypeSymbol.HasFullyQualifiedMetadataName(KeyValuePairName) ||
            !keyValueTypeSymbol.TypeArguments[1].HasFullyQualifiedMetadataName(TypeName))
        {
            return Fallback(new DiagnosticInfo(Diagnostics.InvalidMethodReturnType, syntax.Identifier.GetLocation(), methodSymbol.Name));
        }

        return Results.Success(new SourceModel(
            ns,
            containingTypes,
            hintName,
            signature,
            keyValueTypeSymbol.ToDisplayString(SymbolDisplayFormats.FullyQualifiedNullable),
            keyValueTypeSymbol.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            MethodName: methodName));
    }

    private static bool IsViewIdTargetSyntax(SyntaxNode node) =>
        node is ClassDeclarationSyntax;

    private static Result<EquatableArray<ViewIdModel>> GetViewIdModel(GeneratorAttributeSyntaxContext context)
    {
        if (!IsReferable((INamedTypeSymbol)context.TargetSymbol))
        {
            return new Result<EquatableArray<ViewIdModel>>(
                EquatableArray<ViewIdModel>.Empty,
                new EquatableArray<DiagnosticInfo>([new DiagnosticInfo(Diagnostics.InvalidViewClass, ((ClassDeclarationSyntax)context.TargetNode).Identifier.GetLocation(), context.TargetSymbol.ToDisplayString())]));
        }

        var className = context.TargetSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        var views = new List<ViewIdModel>();
        foreach (var attribute in context.Attributes)
        {
            if (attribute.TryGetConstructorArgument(0, out var id) && (id.Type is not null) && (id.ToCSharpExpression() is { } expression))
            {
                views.Add(new ViewIdModel(className, id.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat), expression));
            }
        }

        return Results.Success(new EquatableArray<ViewIdModel>(views.ToArray()));
    }

    private static bool IsReferable(INamedTypeSymbol type)
    {
        if (type.IsFileLocal)
        {
            return false;
        }

        for (var current = type; current is not null; current = current.ContainingType)
        {
            if (current.DeclaredAccessibility is Accessibility.Private or Accessibility.Protected or Accessibility.ProtectedAndInternal)
            {
                return false;
            }
        }

        return true;
    }

    private static Dictionary<string, DiagnosticInfo> FindHintNameCollisions(ImmutableArray<Result<SourceModel>> sources)
    {
        var collisions = new Dictionary<string, DiagnosticInfo>(StringComparer.Ordinal);
        var firsts = new Dictionary<string, SourceModel>(StringComparer.OrdinalIgnoreCase);
        foreach (var source in sources.SelectValue().OrderBy(static x => x.HintName, StringComparer.Ordinal))
        {
            if (!firsts.TryGetValue(source.HintName, out var first))
            {
                firsts.Add(source.HintName, source);
            }
            else if ((first.HintName != source.HintName) && !collisions.ContainsKey(source.HintName))
            {
                collisions.Add(source.HintName, new DiagnosticInfo(Diagnostics.HintNameCollision, (Location?)null, source.MethodName, first.MethodName));
            }
        }

        return collisions;
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
            .Distinct()
            .GroupBy(static x => x.ViewIdClassFullName)
            .ToDictionary(static x => x.Key, static x => x.ToArray());

        token.ThrowIfCancellationRequested();

        var collisions = FindHintNameCollisions(sourceResults);
        var emitted = new HashSet<string>(StringComparer.Ordinal);
        var builder = ImmutableArray.CreateBuilder<ViewSourceModel>();
        foreach (var source in sourceResults.SelectValue())
        {
            if (collisions.ContainsKey(source.HintName) || !emitted.Add(source.HintName))
            {
                continue;
            }

            var views = !source.IsFallback && viewMap.TryGetValue(source.ViewIdClassFullName, out var matched) ? matched : [];
            builder.Add(new ViewSourceModel(source, new EquatableArray<ViewIdModel>(views)));
        }

        return builder.ToImmutable();
    }

    // ------------------------------------------------------------
    // Generator
    // ------------------------------------------------------------

    private static void Execute(SourceProductionContext context, ViewSourceModel model)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        var builder = new SourceBuilder();
        BuildSource(builder, model.Source, model.Views);

        context.AddSource(model.Source.HintName, builder);
    }

    private static void BuildSource(SourceBuilder builder, SourceModel source, EquatableArray<ViewIdModel> viewIds)
    {
        builder.AutoGenerated();
        builder.EnableNullable();
        builder.Disable("CS0612, CS0618");
        builder.NewLine();

        // namespace
        if (!String.IsNullOrEmpty(source.Namespace))
        {
            builder.Namespace(source.Namespace);
            builder.NewLine();
        }

        // class
        foreach (var containingType in source.ContainingTypes)
        {
            builder
                .Indent()
                .Append(containingType)
                .NewLine();
            builder.BeginScope();
        }

        // method
        builder
            .Indent()
            .Append(source.Signature)
            .NewLine();
        builder.BeginScope();

        if (source.IsFallback)
        {
            builder
                .Indent()
                .Append("throw new global::System.InvalidOperationException();")
                .NewLine();
        }
        else if (viewIds.Count == 0)
        {
            builder
                .Indent()
                .Append("yield break;")
                .NewLine();
        }

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

        for (var i = 0; i < source.ContainingTypes.Count; i++)
        {
            builder.EndScope();
        }
    }
}
