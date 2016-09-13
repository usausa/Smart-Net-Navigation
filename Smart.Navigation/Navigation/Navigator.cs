namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public event EventHandler<NavigationEventArgs> NavigatedFrom;

        public event EventHandler<NavigationEventArgs> NavigatedTo;

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

        // TODO descriptor, from attribute
        public void Register(object id, Type type)
        {
            Register(id, null, type);
        }

        public void Register(object id, object domain, Type type)
        {
            descripters.Add(id, new PageDescripter(id, domain, type));
        }

        // ------------------------------------------------------------
        // INavigator
        // ------------------------------------------------------------

        public void Exit()
        {
            var provider = components.Get<INavigationProvider>();
            for (var i = stacked.Count - 1; i >= 0; i--)
            {
                var page = stacked[i].Page;
                var target = provider.ResolveTarget(page);

                provider.ClosePage(page);

                (page as IDisposable)?.Dispose();
                if (page != target)
                {
                    (target as IDisposable)?.Dispose();
                }
            }

            stacked.Clear();

            Exited?.Invoke(this, EventArgs.Empty);
        }

        public void Forward(object id)
        {
            InternalForward(id, null);
        }

        public void Forward(object id, INavigationParameter parameter)
        {
            InternalForward(id, parameter);
        }

        public void Push(object id)
        {
            InternalPush(id, null);
        }

        public void Push(object id, INavigationParameter parameter)
        {
            InternalPush(id, parameter);
        }

        public void Pop()
        {
            InternalPop(1, null);
        }

        public void Pop(INavigationParameter parameter)
        {
            InternalPop(1, parameter);
        }

        public void Pop(int level)
        {
            InternalPop(level, null);
        }

        public void Pop(int level, INavigationParameter parameter)
        {
            InternalPop(level, parameter);
        }

        public void PopAndForward(object id)
        {
            InternalPopAndForward(id, stacked.Count, null);
        }

        public void PopAndForward(object id, int level)
        {
            InternalPopAndForward(id, level, null);
        }

        public void PopAndForward(object id, INavigationParameter parameter)
        {
            InternalPopAndForward(id, stacked.Count, parameter);
        }

        public void PopAndForward(object id, int level, INavigationParameter parameter)
        {
            InternalPopAndForward(id, level, parameter);
        }

        // ------------------------------------------------------------
        // Internal
        // ------------------------------------------------------------

        private void InternalForward(object id, INavigationParameter parameter)
        {
            var provider = components.Get<INavigationProvider>();
            var context = new NavigationContext(CurrentPageId, id, false, false, parameter ?? EmptyParameter);

            if (!ConfirmNavigation(provider, context))
            {
                return;
            }

            PageDescripter descripter;
            if (!descripters.TryGetValue(id, out descripter))
            {
                return;
            }

            InternalNavigate(new NavigationController(provider, components.Get<IPluginPipeline>(), new PluginContext(components), context), controller =>
            {
                if (CurrentPage != null)
                {
                    ClosePage(controller, CurrentPage);

                    stacked.RemoveAt(stacked.Count - 1);
                }

                stacked.Add(new PageStack(descripter, CreatePage(controller, descripter.Type)));
            });
        }

        private void InternalPush(object id, INavigationParameter parameter)
        {
            var provider = components.Get<INavigationProvider>();
            var context = new NavigationContext(CurrentPageId, id, true, false, parameter ?? EmptyParameter);

            if (!ConfirmNavigation(provider, context))
            {
                return;
            }

            PageDescripter descripter;
            if (!descripters.TryGetValue(id, out descripter))
            {
                return;
            }

            InternalNavigate(new NavigationController(provider, components.Get<IPluginPipeline>(), new PluginContext(components), context), controller =>
            {
                var exist = false;
                var first = -1;
                var last = -1;
                if ((descripter.Domain != null) && !Equals(CurrentPageDomain, descripter.Domain))
                {
                    first = stacked.FindIndex(_ => Equals(_.Descripter.Domain, descripter.Domain));
                    if (first >= 0)
                    {
                        last = stacked.FindLastIndex(_ => Equals(_.Descripter.Domain, descripter.Domain));
                        exist = stacked.Skip(first).Take(last - first + 1).Any(_ => Equals(_.Descripter.Id, descripter.Id));
                    }
                }

                stacked[stacked.Count - 1].RestoreParameter = DeactivePage(controller, CurrentPage);

                if (first >= 0)
                {
                    var temp = new PageStack[last - first + 1];
                    stacked.CopyTo(first, temp, 0, temp.Length);
                    for (var i = 0; i < stacked.Count - last - 1; i++)
                    {
                        stacked[i + first] = stacked[i + last + 1];
                    }
                    for (var i = 0; i < temp.Length; i++)
                    {
                        stacked[stacked.Count - temp.Length + i] = temp[i];
                    }
                }

                if (!exist)
                {
                    stacked.Add(new PageStack(descripter, CreatePage(controller, descripter.Type)));
                }
                else
                {
                    ActivaPage(controller, CurrentPage, stacked[stacked.Count - 1].RestoreParameter);
                    stacked[stacked.Count - 1].RestoreParameter = null;
                }
            });
        }

        private void InternalPop(int level, INavigationParameter parameter)
        {
            if ((level < 1) || (level > stacked.Count - 1))
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            // TODO toPageIdをスタックから決定
            var provider = components.Get<INavigationProvider>();
            var context = new NavigationContext(CurrentPageId, null, false, true, parameter ?? EmptyParameter);

            if (!ConfirmNavigation(provider, context))
            {
                return;
            }

            InternalNavigate(new NavigationController(provider, components.Get<IPluginPipeline>(), new PluginContext(components), context), controller =>
            {
                for (var i = stacked.Count - 1; i >= stacked.Count - level; i--)
                {
                    ClosePage(controller, stacked[i].Page);
                }

                stacked.RemoveRange(stacked.Count - level, level);

                ActivaPage(controller, CurrentPage, stacked[stacked.Count - 1].RestoreParameter);
                stacked[stacked.Count - 1].RestoreParameter = null;
            });
        }

        private void InternalPopAndForward(object id, int level, INavigationParameter parameter)
        {
            if ((level < 1) || (level > stacked.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            var provider = components.Get<INavigationProvider>();
            var context = new NavigationContext(CurrentPageId, id, false, false, parameter ?? EmptyParameter);

            if (!ConfirmNavigation(provider, context))
            {
                return;
            }

            PageDescripter descripter;
            if (!descripters.TryGetValue(id, out descripter))
            {
                return;
            }

            InternalNavigate(new NavigationController(provider, components.Get<IPluginPipeline>(), new PluginContext(components), context), controller =>
            {
                for (var i = stacked.Count - 1; i >= stacked.Count - level; i--)
                {
                    ClosePage(controller, stacked[i].Page);
                }

                stacked.RemoveRange(stacked.Count - level, level);

                stacked.Add(new PageStack(descripter, CreatePage(controller, descripter.Type)));
            });
        }

        // ------------------------------------------------------------
        // Navigation
        // ------------------------------------------------------------

        private void InternalNavigate(
            INavigationController controller,
            Action<INavigationController> updateStack)
        {
            if (controller.Provider.IsAsync)
            {
                controller.Provider.BeginInvoke(() => InternalNavigate(controller, updateStack));
                return;
            }

            var currentPage = CurrentPage;
            if (currentPage != null)
            {
                ProcessNavigatedFrom(controller, currentPage);
            }

            var args = new NavigationEventArgs(controller.NavigationContext);

            NavigatedFrom?.Invoke(this, args);

            updateStack(controller);

            NavigatedTo?.Invoke(this, args);

            ProcessNavigatedTo(controller, CurrentPage);
        }

        // ------------------------------------------------------------
        // Helper
        // ------------------------------------------------------------

        private bool ConfirmNavigation(INavigationProvider provider, INavigationContext context)
        {
            var page = CurrentPage;
            if (page != null)
            {
                var target = provider.ResolveTarget(page);
                var confirm = target as IConfirmRequest;
                if (confirm != null)
                {
                    var operation = new ConfirmOperation();
                    confirm.NavigationConfirm(context, operation);
                    if (operation.Cancel)
                    {
                        return false;
                    }
                }
            }

            var handler = Confirm;
            if (handler != null)
            {
                var args = new ConfirmEventArgs(context);
                handler(this, args);
                if (args.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        // ------------------------------------------------------------
        // Lifecycle
        // ------------------------------------------------------------

        private object CreatePage(INavigationController controller, Type type)
        {
            // TODO static or Controller
            var page = components.Get<IActivator>().Create(type);

            controller.Provider.OpenPage(page);

            var target = controller.Provider.ResolveTarget(page);

            var aware = target as INavigatorAware;
            if (aware != null)
            {
                aware.Navigator = this;
            }

            controller.Pipeline?.OnCreate(controller.PluginContext, page, target);

            return page;
        }

        private static void ClosePage(INavigationController controller, object page)
        {
            var target = controller.Provider.ResolveTarget(page);

            controller.Pipeline?.OnClose(controller.PluginContext, page, target);

            controller.Provider.ClosePage(page);

            (page as IDisposable)?.Dispose();
            if (page != target)
            {
                (target as IDisposable)?.Dispose();
            }
        }

        private static void ActivaPage(INavigationController controller, object page, object parameter)
        {
            controller.Provider.ActivePage(page, parameter);
        }

        private static object DeactivePage(INavigationController controller, object page)
        {
            return controller.Provider.DectivePage(page);
        }

        // ------------------------------------------------------------
        // Navigation
        // ------------------------------------------------------------

        private static void ProcessNavigatedFrom(INavigationController controller, object page)
        {
            var target = controller.Provider.ResolveTarget(page);

            (target as INavigationEventSupport)?.OnNavigatedFrom(controller.NavigationContext);

            controller.Pipeline?.OnNavigatedFrom(controller.PluginContext, page, target);
        }

        private static void ProcessNavigatedTo(INavigationController controller, object page)
        {
            var target = controller.Provider.ResolveTarget(page);

            controller.Pipeline?.OnNavigatedTo(controller.PluginContext, page, target);

            (target as INavigationEventSupport)?.OnNavigatedTo(controller.NavigationContext);
        }
    }
}
