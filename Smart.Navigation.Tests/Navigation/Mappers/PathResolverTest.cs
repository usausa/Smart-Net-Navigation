namespace Smart.Navigation.Mappers
{
    using Xunit;

    public class PathResolverTest
    {
        [Fact]
        public static void ResolveAbsolutePath()
        {
            var resolver = new PathResolver();

            Assert.Equal("/Page1", resolver.Resolve(null, "/Page1"));
            Assert.Equal("/Childs/Child1", resolver.Resolve(null, "/Childs/Child1"));
        }

        [Fact]
        public static void ResolveRelativePathWhenCurrentIsNull()
        {
            var resolver = new PathResolver();

            Assert.Equal("/Page1", resolver.Resolve(null, "Page1"));
            Assert.Equal("/Childs/Child1", resolver.Resolve(null, "Childs/Child1"));
        }

        [Fact]
        public static void ResolveRelativePathWhereSameLevel()
        {
            var resolver = new PathResolver();

            Assert.Equal("/Page2", resolver.Resolve("/Page1", "Page2"));
            Assert.Equal("/Childs/Child2", resolver.Resolve("/Childs/Child1", "Child2"));
        }

        [Fact]
        public static void ResolveRelativePathWhereLoweLevel()
        {
            var resolver = new PathResolver();

            Assert.Equal("/Childs/Child1", resolver.Resolve("/Page1", "Childs/Child1"));
            Assert.Equal("/Childs/GrandChilds/GrandChild1", resolver.Resolve("/Childs/Child1", "GrandChilds/GrandChild1"));
        }

        // TODO 絶対+一部..
        // TODO 相対+..
        // TODO ..が範囲外？
    }
}
