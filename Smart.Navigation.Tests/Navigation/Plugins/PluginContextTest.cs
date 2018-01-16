namespace Smart.Navigation.Plugins
{
    using System;

    using Xunit;

    public static class PluginContextTest
    {
        [Fact]
        public static void TestPluginContextLoadStoredValue()
        {
            var contect = new PluginContext();

            contect.Save(typeof(string), "abc");
            Assert.Equal("abc", contect.Load<string>(typeof(string)));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefault()
        {
            var contect = new PluginContext();

            contect.Save(typeof(string), "abc");
            Assert.Equal("abc", contect.LoadOr(typeof(string), string.Empty));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultNotPrepared()
        {
            var contect = new PluginContext();

            Assert.Equal("abc", contect.LoadOr(typeof(string), "abc"));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultNotContained()
        {
            var contect = new PluginContext();

            contect.Save(typeof(int), 123);
            Assert.Equal("abc", contect.LoadOr(typeof(string), "abc"));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultByFactory()
        {
            var contect = new PluginContext();

            contect.Save(typeof(string), "abc");
            Assert.Equal("abc", contect.LoadOr(typeof(string), () => string.Empty));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultNotPreparedByFactory()
        {
            var contect = new PluginContext();

            Assert.Equal("abc", contect.LoadOr(typeof(string), () => "abc"));
        }

        [Fact]
        public static void TestPluginContextLoadOrDefaultNotContainedByFactory()
        {
            var contect = new PluginContext();

            contect.Save(typeof(int), 123);
            Assert.Equal("abc", contect.LoadOr(typeof(string), () => "abc"));
        }

        [Fact]
        public static void TestPluginContextLoadFaiied()
        {
            var contect = new PluginContext();

            contect.Save(typeof(int), 123);
            Assert.Equal("abc", contect.LoadOr(typeof(string), (Func<string>)null));
        }
    }
}
