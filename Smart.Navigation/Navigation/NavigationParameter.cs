namespace Smart.Navigation
{
    public class NavigationParameter : INavigationParameter
    {
        public T GetValue<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public T GetValue<T>()
        {
            throw new System.NotImplementedException();
        }

        public T GetValueOrDefault<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public T GetValueOrDefault<T>()
        {
            throw new System.NotImplementedException();
        }

        public T GetValueOr<T>(string key, T defaultValue)
        {
            throw new System.NotImplementedException();
        }

        public T GetValueOr<T>(T defaultValue)
        {
            throw new System.NotImplementedException();
        }
    }
}
