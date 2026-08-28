namespace Smart.Navigation.Generator.Tests;

using SourceGenerateHelper.Testing;

public sealed class PipelineCacheTest
{
    private const string Source =
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
            public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();
        }
        """;

    private const string UnrelatedSource =
        """
        namespace Other;

        internal sealed class Unrelated;
        """;

    private const string AddedTargetSource =
        """
        using Smart.Navigation.Attributes;

        namespace Test;

        [View(ViewId.Form1)]
        public sealed class Form1
        {
        }
        """;

    // ------------------------------------------------------------
    // Cache
    // ------------------------------------------------------------

    [Fact]
    public void UnrelatedEditKeepsModelCached()
    {
        // Arrange & Act
        var result = GeneratorTestHelper.RunIncremental(Source, UnrelatedSource);

        // Assert
        Assert.Equal(result.FirstGeneratedText, result.SecondGeneratedText);
        Assert.NotEmpty(result.OutputReasons);
        Assert.DoesNotContain(result.OutputReasons, static x => x.IsChanged());
    }

    [Fact]
    public void TargetEditRebuildsModel()
    {
        // Arrange & Act
        var result = GeneratorTestHelper.RunIncremental(Source, AddedTargetSource);

        // Assert
        Assert.Contains(result.OutputReasons, static x => x.IsChanged());
    }
}
