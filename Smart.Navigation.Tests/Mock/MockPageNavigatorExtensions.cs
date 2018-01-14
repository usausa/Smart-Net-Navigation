namespace Smart.Mock
{
    using Smart.Navigation;

    public static class MockPageNavigatorExtensions
    {
        public static NavigatorConfig UseMockProvider(this NavigatorConfig config)
        {
            return config.UseProvider<MockPageNavigationProvider>();
        }
    }
}
