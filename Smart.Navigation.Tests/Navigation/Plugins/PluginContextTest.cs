namespace Smart.Navigation.Plugins
{
    using System;

    using Xunit;

    public static class PluginContextTest
    {
        [Fact]
        public static void LoadStoredValue()
        {
            var context = new PluginContext();

            context.Save(typeof(string), "abc");
            Assert.Equal("abc", context.Load<string>(typeof(string)));
        }

        [Fact]
        public static void LoadOrDefault()
        {
            var context = new PluginContext();

            context.Save(typeof(string), "abc");
            Assert.Equal("abc", context.LoadOr(typeof(string), string.Empty));
        }

        [Fact]
        public static void LoadOrDefaultNotPrepared()
        {
            var context = new PluginContext();

            Assert.Equal("abc", context.LoadOr(typeof(string), "abc"));
        }

        [Fact]
        public static void LoadOrDefaultNotContained()
        {
            var context = new PluginContext();

            context.Save(typeof(int), 123);
            Assert.Equal("abc", context.LoadOr(typeof(string), "abc"));
        }

        [Fact]
        public static void LoadOrDefaultByFactory()
        {
            var context = new PluginContext();

            context.Save(typeof(string), "abc");
            Assert.Equal("abc", context.LoadOr(typeof(string), () => string.Empty));
        }

        [Fact]
        public static void LoadOrDefaultNotPreparedByFactory()
        {
            var context = new PluginContext();

            Assert.Equal("abc", context.LoadOr(typeof(string), () => "abc"));
        }

        [Fact]
        public static void LoadOrDefaultNotContainedByFactory()
        {
            var context = new PluginContext();

            context.Save(typeof(int), 123);
            Assert.Equal("abc", context.LoadOr(typeof(string), () => "abc"));
        }

        [Fact]
        public static void LoadFailed()
        {
            var context = new PluginContext();

            context.Save(typeof(int), 123);
            Assert.Throws<ArgumentNullException>(() => context.LoadOr(typeof(string), (Func<string>)null));
        }
    }
}
