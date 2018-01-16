namespace Smart.Navigation.Plugins
{
    using System;

    using Xunit;

    public static class PluginContextTest
    {
        [Fact]
        public static void TestPluginContextLoadStoredValue()
        {
            var context = new PluginContext();

            context.Save(typeof(string), "abc");
            Assert.Equal("abc", context.Load<string>(typeof(string)));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefault()
        {
            var context = new PluginContext();

            context.Save(typeof(string), "abc");
            Assert.Equal("abc", context.LoadOr(typeof(string), string.Empty));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultNotPrepared()
        {
            var context = new PluginContext();

            Assert.Equal("abc", context.LoadOr(typeof(string), "abc"));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultNotContained()
        {
            var context = new PluginContext();

            context.Save(typeof(int), 123);
            Assert.Equal("abc", context.LoadOr(typeof(string), "abc"));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultByFactory()
        {
            var context = new PluginContext();

            context.Save(typeof(string), "abc");
            Assert.Equal("abc", context.LoadOr(typeof(string), () => string.Empty));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultNotPreparedByFactory()
        {
            var context = new PluginContext();

            Assert.Equal("abc", context.LoadOr(typeof(string), () => "abc"));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultNotContainedByFactory()
        {
            var context = new PluginContext();

            context.Save(typeof(int), 123);
            Assert.Equal("abc", context.LoadOr(typeof(string), () => "abc"));
        }

        [Fact]
        public static void TestPluginContextLoadFaiied()
        {
            var context = new PluginContext();

            context.Save(typeof(int), 123);
            Assert.Throws<ArgumentNullException>(() => context.LoadOr(typeof(string), (Func<string>)null));
        }
    }
}
