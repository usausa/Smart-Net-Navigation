// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Smart.Mock
{
    using System.Diagnostics.CodeAnalysis;

    public abstract class MockWindow
    {
        [AllowNull]
        public object Context { get; set; }

        public bool IsVisible { get; set; }

        public object? Focused { get; set; }
    }
}
