namespace Smart.Mock
{
    using Smart.Navigation;

    public static class MockViewNavigatorExtensions
    {
        public static NavigatorConfig UseMockViewProvider(this NavigatorConfig config)
        {
            return config.UseProvider<MockViewNavigationProvider>();
        }
    }
}
