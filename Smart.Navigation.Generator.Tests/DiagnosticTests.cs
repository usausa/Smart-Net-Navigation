namespace Smart.Navigation.Generator.Tests;

public sealed class DiagnosticTests
{
    [Fact]
    public void Snv0001NonStaticMethodEmitsDiagnostic()
    {
        const string source =
            """
            using System;
            using System.Collections.Generic;
            using Smart.Navigation.Attributes;

            namespace Test;

            public enum ViewId
            {
                Form1
            }

            public partial class ViewRegistry
            {
                [ViewSource]
                public partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();
            }
            """;

        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        Assert.Contains(diagnostics, d => d.Id == "SNV0001");
    }

    [Fact]
    public void Snv0001NonPartialMethodEmitsDiagnostic()
    {
        const string source =
            """
            using System;
            using System.Collections.Generic;
            using Smart.Navigation.Attributes;

            namespace Test;

            public enum ViewId
            {
                Form1
            }

            public static class ViewRegistry
            {
                [ViewSource]
                public static IEnumerable<KeyValuePair<ViewId, Type>> ListViews() => null!;
            }
            """;

        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        Assert.Contains(diagnostics, d => d.Id == "SNV0001");
    }

    [Fact]
    public void Snv0002MethodWithParameterEmitsDiagnostic()
    {
        const string source =
            """
            using System;
            using System.Collections.Generic;
            using Smart.Navigation.Attributes;

            namespace Test;

            public enum ViewId
            {
                Form1
            }

            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews(int dummy);
            }
            """;

        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        Assert.Contains(diagnostics, d => d.Id == "SNV0002");
    }

    [Fact]
    public void Snv0003InvalidReturnTypeEmitsDiagnostic()
    {
        const string source =
            """
            using System;
            using System.Collections.Generic;
            using Smart.Navigation.Attributes;

            namespace Test;

            public enum ViewId
            {
                Form1
            }

            public static partial class ViewRegistry
            {
                [ViewSource]
                public static partial IEnumerable<int> ListViews();
            }
            """;

        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        Assert.Contains(diagnostics, d => d.Id == "SNV0003");
    }
}
