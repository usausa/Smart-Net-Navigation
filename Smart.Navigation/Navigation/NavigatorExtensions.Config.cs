//namespace Smart.Navigation
//{
//    using System;

//    using Smart.Navigation.Components;
//    using Smart.Navigation.Plugins;

//    /// <summary>
//    ///
//    /// </summary>
//    public static partial class NavigatorExtensions
//    {
//        public static Navigator UseProvider(this Navigator navigator, INavigationProvider provider)
//        {
//            if (navigator == null)
//            {
//                throw new ArgumentNullException(nameof(navigator));
//            }

//            if (provider == null)
//            {
//                throw new ArgumentNullException(nameof(provider));
//            }

//            navigator.Configure(c => c.Register(provider));

//            return navigator;
//        }

//        public static Navigator UseActivator(this Navigator navigator, IActivator activator)
//        {
//            if (navigator == null)
//            {
//                throw new ArgumentNullException(nameof(navigator));
//            }

//            if (activator == null)
//            {
//                throw new ArgumentNullException(nameof(activator));
//            }

//            navigator.Configure(c => c.Register(activator));

//            return navigator;
//        }

//        public static Navigator UseActivator(this Navigator navigator, Func<Type, object> callback)
//        {
//            if (callback == null)
//            {
//                throw new ArgumentNullException(nameof(callback));
//            }

//            return navigator.UseActivator(new CallbackActivator(callback));
//        }

//        public static Navigator UseConverter(this Navigator navigator, IConverter converter)
//        {
//            if (navigator == null)
//            {
//                throw new ArgumentNullException(nameof(navigator));
//            }

//            if (converter == null)
//            {
//                throw new ArgumentNullException(nameof(converter));
//            }

//            navigator.Configure(c => c.Register(converter));

//            return navigator;
//        }

//        public static Navigator UseConverter(this Navigator navigator, Func<object, Type, object> callback)
//        {
//            if (callback == null)
//            {
//                throw new ArgumentNullException(nameof(callback));
//            }

//            return navigator.UseConverter(new CallbackConverter(callback));
//        }

//        public static Navigator UsePlugin(this Navigator navigator, IPlugin plugin)
//        {
//            if (navigator == null)
//            {
//                throw new ArgumentNullException(nameof(navigator));
//            }

//            if (plugin == null)
//            {
//                throw new ArgumentNullException(nameof(plugin));
//            }

//            navigator.Configure(c => c.Get<IPluginPipeline>().Plugins.Add(plugin));

//            return navigator;
//        }
//    }
//}
