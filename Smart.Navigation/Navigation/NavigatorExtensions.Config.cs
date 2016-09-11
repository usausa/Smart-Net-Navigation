namespace Smart.Navigation
{
    using System;

    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;

    /// <summary>
    ///
    /// </summary>
    public static partial class NavigatorExtensions
    {
        public static Navigator UseProvider(this Navigator navigator, INavigationProvider provider)
        {
            if (navigator == null)
            {
                throw new ArgumentNullException(nameof(navigator));
            }

            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            navigator.Configure(_ => _.Register(provider));

            return navigator;
        }

        public static Navigator UseActivator(this Navigator navigator, IActivator activator)
        {
            if (navigator == null)
            {
                throw new ArgumentNullException(nameof(navigator));
            }

            if (activator == null)
            {
                throw new ArgumentNullException(nameof(activator));
            }

            navigator.Configure(_ => _.Register(activator));

            return navigator;
        }

        public static Navigator UseActivator(this Navigator navigator, Func<Type, object> callback)
        {
            return navigator.UseActivator(new CallbackActivator(callback));
        }

        public static Navigator UseConverter(this Navigator navigator, IConverter converter)
        {
            if (navigator == null)
            {
                throw new ArgumentNullException(nameof(navigator));
            }

            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            navigator.Configure(_ => _.Register(converter));

            return navigator;
        }

        public static Navigator UsePlugin(this Navigator navigator, IPlugin plugin)
        {
            if (navigator == null)
            {
                throw new ArgumentNullException(nameof(navigator));
            }

            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }

            navigator.Configure(_ => _.Get<IPluginPipeline>().Plugins.Add(plugin));

            return navigator;
        }
    }
}
