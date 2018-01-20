namespace Smart.Mock
{
    using Smart.Navigation;

    public static class MockWindowNavigatorConfigExtensions
    {
        public static NavigatorConfig UseMockWindowProvider(this NavigatorConfig config)
        {
            return config.UseProvider<MockWindowNavigationProvider>();
        }
    }
}
