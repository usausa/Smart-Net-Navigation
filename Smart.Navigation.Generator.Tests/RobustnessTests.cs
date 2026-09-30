namespace Smart.Navigation.Generator.Tests;

using System.Reflection;

using Microsoft.CodeAnalysis;

public class RobustnessTests
{
    private const string Head =
        """
        #nullable enable
        using System;
        using System.Collections.Generic;
        using Smart.Navigation.Attributes;

        namespace Test;

        public enum ViewId
        {
            Form1,
            Form2
        }

        [View(ViewId.Form1)]
        public sealed class Form1
        {
        }

        """;

    // ------------------------------------------------------------
    // Attributes being typed
    // ------------------------------------------------------------

    [Fact]
    public void IncompleteViewAttributeDoesNotStopGeneration()
    {
        const string source = Head + """
            [View]
            public sealed class Form2
            {
            }

            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();
            }
            """;

        Assert.DoesNotContain("CS8785", GeneratorTestHelper.GetProblemIds(source));
        Assert.Contains("typeof(global::Test.Form1)", GeneratorTestHelper.GetAllGeneratedSource(source), StringComparison.Ordinal);
    }

    // ------------------------------------------------------------
    // Declaration
    // ------------------------------------------------------------

    [Theory]
    [InlineData("public static partial class Outer", "public static partial class ViewRegistry", "public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();")]
    [InlineData("public static partial class Outer", "public partial record ViewRegistry", "public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();")]
    [InlineData("public static partial class Outer", "internal static partial class ViewRegistry", "internal static partial IEnumerable<KeyValuePair<ViewId, Type?>> ListViews();")]
    [InlineData("public static partial class Outer", "public static partial class ViewRegistry", "public static partial IEnumerable<KeyValuePair<ViewId, Type>> @event();")]
    public void ImplementationRepeatsDeclaration(string outer, string host, string method)
    {
        var source = Head + $$"""
            {{outer}}
            {
                {{host}}
                {
                    [ViewSource]
                    {{method}}
                }
            }
            """;

        Assert.Empty(GeneratorTestHelper.GetProblemIds(source));
    }

    [Fact]
    public void Snv0001ImplementedMethodEmitsDiagnostic()
    {
        const string source = Head + """
            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();

                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews() => [];
            }
            """;

        Assert.Equal(["SNV0001"], GeneratorTestHelper.GetProblemIds(source));
    }

    [Fact]
    public void ErrorGeneratesThrowingImplementation()
    {
        const string source = Head + """
            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews(int value);
            }
            """;

        Assert.Equal(["SNV0002"], GeneratorTestHelper.GetProblemIds(source));
        Assert.Contains("throw new global::System.InvalidOperationException();", GeneratorTestHelper.GetAllGeneratedSource(source), StringComparison.Ordinal);
    }

    // ------------------------------------------------------------
    // Diagnostics
    // ------------------------------------------------------------

    [Fact]
    public void ErrorsCannotBeSuppressed()
    {
        var descriptors = typeof(NavigationGenerator).Assembly.GetType("Smart.Navigation.Generator.Diagnostics", throwOnError: true)!
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(static x => x.PropertyType == typeof(DiagnosticDescriptor))
            .Select(static x => (DiagnosticDescriptor)x.GetValue(null)!)
            .ToList();

        Assert.All(
            descriptors.Where(static x => x.DefaultSeverity == DiagnosticSeverity.Error),
            static x => Assert.Equal([WellKnownDiagnosticTags.NotConfigurable, WellKnownDiagnosticTags.Compiler], x.CustomTags));
    }

    // ------------------------------------------------------------
    // Views
    // ------------------------------------------------------------

    [Theory]
    [InlineData("public static class Outer { [View(ViewId.Form2)] private sealed class Form2 { } }")]
    [InlineData("[View(ViewId.Form2)] file sealed class Form2 { }")]
    public void Snv0004ViewThatCannotBeReferredToEmitsDiagnostic(string view)
    {
        var source = Head + view + """

            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();
            }
            """;

        Assert.Equal(["SNV0004"], GeneratorTestHelper.GetProblemIds(source));
    }

    [Fact]
    public void ViewOnTwoPartialDeclarationsIsRegisteredOnce()
    {
        const string source = Head + """
            [View(ViewId.Form2)]
            public sealed partial class Form2
            {
            }

            [View(ViewId.Form2)]
            public sealed partial class Form2
            {
            }

            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();
            }
            """;

        var generated = GeneratorTestHelper.GetAllGeneratedSource(source);

        Assert.Equal(2, generated.Split("typeof(global::Test.Form2)").Length);
    }

    [Fact]
    public void ObsoleteViewCompilesWithoutWarning()
    {
        const string source = Head + """
            [Obsolete]
            [View(ViewId.Form2)]
            public sealed class Form2
            {
            }

            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();
            }
            """;

        Assert.Empty(GeneratorTestHelper.GetProblemIds(source));
    }

    [Fact]
    public void Snv0005CaseOnlyMethodNamesGenerateTheFirstOnly()
    {
        const string source = Head + """
            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();

                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> listViews();
            }
            """;

        var problems = GeneratorTestHelper.GetProblemIds(source);

        Assert.Contains("SNV0005", problems);
        Assert.DoesNotContain("CS8785", problems);
    }
}
