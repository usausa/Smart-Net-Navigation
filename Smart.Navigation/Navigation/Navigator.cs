namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;

    using Smart.ComponentModel;
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;
    using Smart.Navigation.Plugins.Parameter;
    using Smart.Navigation.Plugins.Scope;

    /// <summary>
    ///
    /// </summary>
    public class Navigator : DisposableObject, INavigator
    {
        // ------------------------------------------------------------
        // Event
        // ------------------------------------------------------------

        public event EventHandler<ConfirmEventArgs> Confirm;

        public event EventHandler<NavigationEventArgs> Navigating;

        public event EventHandler<EventArgs> Exited;

        // ------------------------------------------------------------
        // Member
        // ------------------------------------------------------------

        private static readonly NavigationParameter EmptyParameter = new NavigationParameter();

        private readonly Dictionary<object, PageDescripter> descripters = new Dictionary<object, PageDescripter>();

        private readonly List<PageStack> stacked = new List<PageStack>();

        private readonly ComponentContainer components = new ComponentContainer();

        // ------------------------------------------------------------
        // Property
        // ------------------------------------------------------------

        private PageStack CurrentStack => stacked.Count > 0 ? stacked[stacked.Count - 1] : null;

        /// <summary>
        ///
        /// </summary>
        public int StackedCount => stacked.Count;

        /// <summary>
        ///
        /// </summary>
        public object CurrentPageId => CurrentStack?.Descripter.Id;

        /// <summary>
        ///
        /// </summary>
        public object CurrentPageDomain => CurrentStack?.Descripter.Domain;

        /// <summary>
        ///
        /// </summary>
        public object CurrentPage => CurrentStack?.Page;

        /// <summary>
        ///
        /// </summary>
        public object CurrentTarget => CurrentPage.NullOr(components.Get<INavigationProvider>().ResolveTarget);

        private object CurrentRestoreParameter
        {
            get { return CurrentStack?.RestoreParameter; }
            set
            {
                var stack = CurrentStack;
                if (stack != null)
                {
                    stack.RestoreParameter = value;
                }
            }
        }

        // ------------------------------------------------------------
        // Configuration
        // ------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        public Navigator()
        {
            components.Register<IActivator>(new StandardActivator());
            components.Register<IConverter>(new StandardConverter());
            components.Register<IPluginPipeline>(new PluginPipeline(
                new ParameterPlugin(),
                new ScopePlugin()));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // ------------------------------------------------------------
        // Configuration
        // ------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        public void Configure(Action<ComponentContainer> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(components);
        }

        // ------------------------------------------------------------
        // Registration
        // ------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        public void Register(object id, Type type)
        {
            Register(id, null, type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="domain"></param>
        /// <param name="type"></param>
        public void Register(object id, object domain, Type type)
        {
            descripters.Add(id, new PageDescripter(id, domain, type));
        }

        // ------------------------------------------------------------
        // INavigator
        // ------------------------------------------------------------

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Forward(object id)
        {
            throw new NotImplementedException();
        }

        public void Forward(object id, INavigationParameter parameters)
        {
            throw new NotImplementedException();
        }

        public void Push(object id)
        {
            throw new NotImplementedException();
        }

        public void Push(object id, INavigationParameter parameters)
        {
            throw new NotImplementedException();
        }

        public void Pop()
        {
            throw new NotImplementedException();
        }

        public void Pop(INavigationParameter parameters)
        {
            throw new NotImplementedException();
        }

        public void Pop(int level)
        {
            throw new NotImplementedException();
        }

        public void Pop(int level, INavigationParameter parameters)
        {
            throw new NotImplementedException();
        }

        public void PopAndForward(object id)
        {
            throw new NotImplementedException();
        }

        public void PopAndForward(object id, int level)
        {
            throw new NotImplementedException();
        }

        public void PopAndForward(object id, INavigationParameter parameters)
        {
            throw new NotImplementedException();
        }

        public void PopAndForward(object id, int level, INavigationParameter parameters)
        {
            throw new NotImplementedException();
        }
    }
}
