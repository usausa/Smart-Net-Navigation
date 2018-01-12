namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.ComponentModel;
    using Smart.Functional;
    using Smart.Navigation.Plugins;
    using Smart.Navigation.Strategies;

    /// <summary>
    ///
    /// </summary>
    public class Navigator : DisposableObject, INavigator
    {
        // ------------------------------------------------------------
        // Event
        // ------------------------------------------------------------

        public event EventHandler<ConfirmEventArgs> Confirm;

        public event EventHandler<NavigationEventArgs> NavigatedFrom;

        public event EventHandler<NavigationEventArgs> NavigatedTo;

        public event EventHandler<EventArgs> Exited;

        // ------------------------------------------------------------
        // Member
        // ------------------------------------------------------------

        private static readonly NavigationParameter EmptyParameter = new NavigationParameter();

        private readonly ComponentContainer components;

        private readonly Dictionary<object, IPageDescriptor> descriptors = new Dictionary<object, IPageDescriptor>();

        private readonly PageStackManager stackManager = new PageStackManager();

        private readonly INavigationProvider provider;

        private readonly IPlugin[] plugins;

        // ------------------------------------------------------------
        // Property
        // ------------------------------------------------------------

        public int StackedCount => stackManager.Stacked.Count;

        public object CurrentPageId => stackManager.CurrentPageId;

        public object CurrentPageDomain => stackManager.CurrentPageDomain;

        public object CurrentPage => stackManager.CurrentPage;

        public object CurrentTarget => stackManager.CurrentPage.MapOrDefalut(x => provider.ResolveTarget(x));

        // ------------------------------------------------------------
        // Constructor
        // ------------------------------------------------------------

        public Navigator()
            : this(new NavigatorConfig())
        {
        }

        public Navigator(INavigatorConfig config)
        {
            components = config.ResolveComponents();

            provider = components.Get<INavigationProvider>();
            plugins = components.GetAll<IPlugin>().ToArray();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // ------------------------------------------------------------
        // Registration
        // ------------------------------------------------------------

        public void Register(IPageDescriptor descriptor)
        {
            descriptors.Add(descriptor.Id, descriptor);
        }

        // ------------------------------------------------------------
        // Navigation
        // ------------------------------------------------------------

        public void Exit()
        {
            for (var i = stackManager.Stacked.Count - 1; i >= 0; i--)
            {
                var page = stackManager.Stacked[i].Page;
                var target = provider.ResolveTarget(page);

                provider.ClosePage(page);

                (page as IDisposable)?.Dispose();
                if (page != target)
                {
                    (target as IDisposable)?.Dispose();
                }
            }

            stackManager.Stacked.Clear();

            Exited?.Invoke(this, EventArgs.Empty);
        }

        public void Navigate(INavigationStrategy strategy, INavigationParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        // ------------------------------------------------------------
        // Helper
        // ------------------------------------------------------------
    }
}
