namespace Smart.Navigation.Generator.Helpers;

using Microsoft.CodeAnalysis;

internal static class RoslynExtensions
{
    public static string ToText(this Accessibility accessibility) => accessibility switch
    {
        Accessibility.Public => "public",
        Accessibility.Protected => "protected",
        Accessibility.Private => "private",
        Accessibility.Internal => "internal",
        Accessibility.ProtectedOrInternal => "protected internal",
        Accessibility.ProtectedAndInternal => "private protected",
        _ => throw new NotSupportedException()
    };
}
