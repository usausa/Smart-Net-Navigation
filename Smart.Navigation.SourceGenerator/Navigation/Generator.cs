namespace Smart.Navigation;

using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

[Generator]
public sealed class Generator : IIncrementalGenerator
{
    // ------------------------------------------------------------
    // Model
    // ------------------------------------------------------------

    internal sealed record ViewIdEntry(
        string ViewIdFullName,
        object? Value);

    internal sealed record ViewModel(
        string ClassFullName,
        string ViewIdClassFullName,
        ViewIdEntry[] Entries);

    internal sealed record SourceModel(
        string Namespace,
        string ClassName,
        bool IsValueType,
        Accessibility MethodAccessibility,
        string MethodName,
        string ReturnTypeName,
        string EntryTypeName,
        string ViewIdClassFullName);

    // ------------------------------------------------------------
    // Generator
    // ------------------------------------------------------------

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(AddAttribute);

        var viewProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => IsViewTargetSyntax(node),
                static (context, _) => GetViewModel(context))
            .Where(static x => x is not null)
            .Collect();

        var sourceProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => IsSourceTargetSyntax(node),
                static (context, _) => GetSourceModel(context))
            .Where(static x => x is not null)
            .Collect();

        context.RegisterImplementationSourceOutput(
            viewProvider.Combine(sourceProvider),
            static (context, provider) => Execute(context, provider.Left!, provider.Right!));
    }

    private static bool IsViewTargetSyntax(SyntaxNode node) =>
        node is ClassDeclarationSyntax { AttributeLists.Count: > 0 };

    private static bool IsSourceTargetSyntax(SyntaxNode node) =>
        node is MethodDeclarationSyntax { AttributeLists.Count: > 0 };

    private static ViewModel? GetViewModel(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
        if (ModelExtensions.GetDeclaredSymbol(context.SemanticModel, classDeclarationSyntax) is not ITypeSymbol typeSymbol)
        {
            return null;
        }

        var attributes = typeSymbol.GetAttributes()
            .Where(static x => x.AttributeClass!.ToDisplayString() == "Smart.Navigation.Attributes.ViewAttribute" &&
                               x.ConstructorArguments.Length == 1)
            .Select(static x => x.ConstructorArguments[0])
            .ToList();
        if (attributes.Count == 0)
        {
            return null;
        }

        return new ViewModel(
            typeSymbol.ToDisplayString(),
            attributes[0].Type!.ToDisplayString(),
            attributes.Select(static x => new ViewIdEntry(x.ToCSharpString(), x.Value)).ToArray());
    }

    private static SourceModel? GetSourceModel(GeneratorSyntaxContext context)
    {
        var methodDeclarationSyntax = (MethodDeclarationSyntax)context.Node;
        if (methodDeclarationSyntax.ParameterList.Parameters.Count != 0)
        {
            return null;
        }

        var methodSymbol = context.SemanticModel.GetDeclaredSymbol(methodDeclarationSyntax);
        if ((methodSymbol is null) || !methodSymbol.IsPartialDefinition || !methodSymbol.IsStatic)
        {
            return null;
        }

        var attribute = methodSymbol.GetAttributes()
            .FirstOrDefault(static x => x.AttributeClass!.ToDisplayString() == "Smart.Navigation.Attributes.ViewSource");
        if (attribute is null)
        {
            return null;
        }

        if ((methodSymbol.ReturnType is not INamedTypeSymbol returnTypeSymbol) ||
            (returnTypeSymbol.ConstructedFrom.ToDisplayString() != "System.Collections.Generic.IEnumerable<T>"))
        {
            return null;
        }

        if ((returnTypeSymbol.TypeArguments[0] is not INamedTypeSymbol keyValueTypeSymbol) ||
            (keyValueTypeSymbol.ConstructedFrom.ToDisplayString() != "System.Collections.Generic.KeyValuePair<TKey, TValue>"))
        {
            return null;
        }

        var containingType = methodSymbol.ContainingType;
        var ns = String.IsNullOrEmpty(containingType.ContainingNamespace.Name)
            ? string.Empty
            : containingType.ContainingNamespace.ToDisplayString();

        return new SourceModel(
            ns,
            containingType.Name,
            containingType.IsValueType,
            methodSymbol.DeclaredAccessibility,
            methodSymbol.Name,
            returnTypeSymbol.ToDisplayString(),
            keyValueTypeSymbol.ToDisplayString(),
            keyValueTypeSymbol.TypeArguments[0].ToDisplayString());
    }

    // ------------------------------------------------------------
    // Builder
    // ------------------------------------------------------------

    private static void Execute(SourceProductionContext context, ImmutableArray<ViewModel> viewModels, ImmutableArray<SourceModel> sourceModels)
    {
        var viewModelMap = viewModels
            .GroupBy(static x => x.ViewIdClassFullName)
            .ToDictionary(static x => x.Key, static x => x.ToList());

        var buffer = new StringBuilder();

        foreach (var sourceModel in sourceModels)
        {
            buffer.Clear();

            buffer.AppendLine("// <auto-generated />");
            buffer.AppendLine("#nullable enable");

            // namespace
            if (!String.IsNullOrEmpty(sourceModel.Namespace))
            {
                buffer.Append("namespace ").Append(sourceModel.Namespace).AppendLine();
            }

            buffer.AppendLine("{");

            // class
            buffer.Append("    partial ").Append(sourceModel.IsValueType ? "struct " : "class ").Append(sourceModel.ClassName).AppendLine();
            buffer.AppendLine("    {");

            // method
            buffer.Append("        ");
            buffer.Append(ToAccessibilityText(sourceModel.MethodAccessibility));
            buffer.Append(" static partial ");
            buffer.Append(sourceModel.ReturnTypeName);
            buffer.Append(' ');
            buffer.Append(sourceModel.MethodName);
            buffer.Append("()");
            buffer.AppendLine();

            buffer.AppendLine("        {");

            if (viewModelMap.TryGetValue(sourceModel.ViewIdClassFullName, out var views))
            {
                foreach (var entry in views.SelectMany(static x => x.Entries.Select(y => new { Model = x, Entry = y })).OrderBy(static x => x.Entry.Value))
                {
                    buffer.Append("            yield return new ");
                    buffer.Append(sourceModel.EntryTypeName);
                    buffer.Append('(');
                    buffer.Append(entry.Entry.ViewIdFullName);
                    buffer.Append(", ");
                    buffer.Append("typeof(");
                    buffer.Append(entry.Model.ClassFullName);
                    buffer.Append("));");
                    buffer.AppendLine();
                }
            }

            buffer.AppendLine("        }");

            buffer.AppendLine("    }");
            buffer.AppendLine("}");

            var source = buffer.ToString();
            var filename = MakeRegistryFilename(buffer, sourceModel.Namespace, sourceModel.ClassName);
            context.AddSource(filename, SourceText.From(source, Encoding.UTF8));
        }
    }

    private static string MakeRegistryFilename(StringBuilder buffer, string ns, string className)
    {
        buffer.Clear();

        if (!String.IsNullOrEmpty(ns))
        {
            buffer.Append(ns.Replace('.', '_'));
            buffer.Append('_');
        }

        buffer.Append(className);
        buffer.Append(".g.cs");

        return buffer.ToString();
    }

    private static string ToAccessibilityText(Accessibility accessibility) => accessibility switch
    {
        Accessibility.Public => "public",
        Accessibility.Protected => "protected",
        Accessibility.Private => "private",
        Accessibility.Internal => "internal",
        Accessibility.ProtectedOrInternal => "protected internal",
        Accessibility.ProtectedAndInternal => "private protected",
        _ => throw new NotSupportedException()
    };

    // ------------------------------------------------------------
    // Attribute
    // ------------------------------------------------------------

    private const string AttributeSource = @"// <auto-generated />
using System;

namespace Smart.Navigation.Attributes
{
    [System.Diagnostics.Conditional(""COMPILE_TIME_ONLY"")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ViewSource : Attribute
    {
    }
}
";

    private static void AddAttribute(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource("ViewSource", SourceText.From(AttributeSource, Encoding.UTF8));
    }
}
