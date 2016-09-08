namespace Smart.Navigation.Plugins
{
    using System;

    using Smart.ComponentModel;

    public interface IPluginContext
    {
        IComponentContainer Components { get; }

        void Save<T>(Type type, T value);

        T Load<T>(Type type);

        T LoadOr<T>(Type type, T defaultValue);

        T LoadOr<T>(Type type, Func<T> defaultValueFactory);
    }
}
