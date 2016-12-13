namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.ComponentModel;
    using Smart.Functional;
    using Smart.Navigation.Components;
    using Smart.Navigation.Plugins;
    using Smart.Navigation.Plugins.Parameter;
    using Smart.Navigation.Plugins.Scope;
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

        private readonly Dictionary<object, IPageDescriptor> descriptors = new Dictionary<object, IPageDescriptor>();

        private readonly PageStackManager stackManager = new PageStackManager();

        private readonly ComponentContainer components;

        // ------------------------------------------------------------
        // Property
        // ------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        public int StackedCount => stackManager.Stacked.Count;

        /// <summary>
        ///
        /// </summary>
        public object CurrentPageId => stackManager.CurrentPageId;

        /// <summary>
        ///
        /// </summary>
        public object CurrentPageDomain => stackManager.CurrentPageDomain;

        /// <summary>
        ///
        /// </summary>
        public object CurrentPage => stackManager.CurrentPage;

        /// <summary>
        ///
        /// </summary>
        public object CurrentTarget => stackManager.CurrentPage.Or(components.Get<INavigationProvider>().ResolveTarget);

        // ------------------------------------------------------------
        // Configuration
        // ------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="components"></param>
        public Navigator(ComponentContainer components)
        {
            this.components = components;
            //components.Register<IActivator>(new StandardActivator());
            //components.Register<IConverter>(new StandardConverter());
            //components.Register<IPluginPipeline>(new PluginPipeline(
            //    new ParameterPlugin(),
            //    new ScopePlugin()));
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

        public void Register(IPageDescriptor descriptor)
        {
            descriptors.Add(descriptor.Id, descriptor);
        }

        // ------------------------------------------------------------
        // INavigator
        // ------------------------------------------------------------

        public void Exit()
        {
            var provider = components.Get<INavigationProvider>();
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
            // TODO
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
            InternalPopAndForward(id, stackManager.Stacked.Count, null);
        }

        public void PopAndForward(object id, int level)
        {
            InternalPopAndForward(id, level, null);
        }

        public void PopAndForward(object id, INavigationParameter parameter)
        {
            InternalPopAndForward(id, stackManager.Stacked.Count, parameter);
        }

        public void PopAndForward(object id, int level, INavigationParameter parameter)
        {
            InternalPopAndForward(id, level, parameter);
        }

        // ------------------------------------------------------------
        // Internal
        // ------------------------------------------------------------

        // TODO Stacked と IPageDescriptorの処理をController内へ

        private void InternalForward(object id, INavigationParameter parameter)
        {
            // TODO controller
            var context = new NavigationContext(CurrentPageId, id, false, false, parameter ?? EmptyParameter);
            var controller = new NavigationController(components, new PluginContext(components), context, this, stackManager);

            if (!ConfirmNavigation(controller))
            {
                return;
            }

            IPageDescriptor descriptor;
            if (!descriptors.TryGetValue(id, out descriptor))
            {
                return;
            }

            InternalNavigate(controller, _ => InternalForwardAction(_, descriptor));
        }

        // TODO Refactor
        private static void InternalForwardAction(INavigationController controller, IPageDescriptor descriptor)
        {
            // TODO メソッド化？
            if (controller.StackManager.CurrentPage != null)
            {
                controller.ClosePage(controller.StackManager.CurrentPage);

                controller.StackManager.Stacked.RemoveAt(controller.StackManager.Stacked.Count - 1);
            }

            controller.StackManager.Stacked.Add(new PageStack(descriptor, controller.CreatePage(descriptor.Type)));
        }

        private void InternalPush(object id, INavigationParameter parameter)
        {
            // TODO controller
            var context = new NavigationContext(CurrentPageId, id, true, false, parameter ?? EmptyParameter);
            var controller = new NavigationController(components, new PluginContext(components), context, this, stackManager);

            if (!ConfirmNavigation(controller))
            {
                return;
            }

            IPageDescriptor descriptor;
            if (!descriptors.TryGetValue(id, out descriptor))
            {
                return;
            }

            InternalNavigate(controller, _ =>
                    InternalPushAction(_, descriptor));
        }

        // TODO CurrentPageDomainの扱い？
        private static void InternalPushAction(INavigationController controller, IPageDescriptor descriptor)
        {
            var exist = false;
            var first = -1;
            var last = -1;
            if ((descriptor.Domain != null) && !Equals(controller.StackManager.CurrentPageDomain, descriptor.Domain))
            {
                first = controller.StackManager.Stacked.FindIndex(_ => Equals(_.Descriptor.Domain, descriptor.Domain));
                if (first >= 0)
                {
                    last = controller.StackManager.Stacked.FindLastIndex(_ => Equals(_.Descriptor.Domain, descriptor.Domain));
                    exist = controller.StackManager.Stacked.Skip(first).Take(last - first + 1).Any(_ => Equals(_.Descriptor.Id, descriptor.Id));
                }
            }

            controller.DeactivePage();

            if (first >= 0)
            {
                var temp = new PageStack[last - first + 1];
                controller.StackManager.Stacked.CopyTo(first, temp, 0, temp.Length);
                for (var i = 0; i < controller.StackManager.Stacked.Count - last - 1; i++)
                {
                    controller.StackManager.Stacked[i + first] = controller.StackManager.Stacked[i + last + 1];
                }

                for (var i = 0; i < temp.Length; i++)
                {
                    controller.StackManager.Stacked[controller.StackManager.Stacked.Count - temp.Length + i] = temp[i];
                }
            }

            if (!exist)
            {
                controller.StackManager.Stacked.Add(new PageStack(descriptor, controller.CreatePage(descriptor.Type)));
            }
            else
            {
                controller.ActivaPage();
                controller.StackManager.CurrentStack.RestoreParameter = null;
            }
        }

        private void InternalPop(int level, INavigationParameter parameter)
        {
            if ((level < 1) || (level > stackManager.Stacked.Count - 1))
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            // TODO controller
            // TODO toPageIdをスタックから決定
            var context = new NavigationContext(CurrentPageId, null, false, true, parameter ?? EmptyParameter);
            var controller = new NavigationController(components, new PluginContext(components), context, this, stackManager);

            if (!ConfirmNavigation(controller))
            {
                return;
            }

            InternalNavigate(controller, _ => InternalPopAction(_, level));
        }

        // TODO Refactor
        private static void InternalPopAction(INavigationController controller, int level)
        {
            // TODO メソッド化？
            for (var i = controller.StackManager.Stacked.Count - 1; i >= controller.StackManager.Stacked.Count - level; i--)
            {
                controller.ClosePage(controller.StackManager.Stacked[i].Page);
            }

            controller.StackManager.Stacked.RemoveRange(controller.StackManager.Stacked.Count - level, level);

            // TODO 内部化
            controller.ActivaPage();
            controller.StackManager.CurrentStack.RestoreParameter = null;
        }

        private void InternalPopAndForward(object id, int level, INavigationParameter parameter)
        {
            if ((level < 1) || (level > stackManager.Stacked.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            // TODO providerがControllerと重複、別メソッドかした上でcontrollerにインスタンスはここで定義
            var context = new NavigationContext(CurrentPageId, id, false, false, parameter ?? EmptyParameter);
            var controller = new NavigationController(components, new PluginContext(components), context, this, stackManager);

            if (!ConfirmNavigation(controller))
            {
                return;
            }

            IPageDescriptor descriptor;
            if (!descriptors.TryGetValue(id, out descriptor))
            {
                return;
            }

            InternalNavigate(controller, _ => InternalPopAndForwardAction(_, level, descriptor));
        }

        // TODO Redactor
        private static void InternalPopAndForwardAction(INavigationController controller, int level, IPageDescriptor descriptor)
        {
            // TODO メソッド化？
            for (var i = controller.StackManager.Stacked.Count - 1; i >= controller.StackManager.Stacked.Count - level; i--)
            {
                controller.ClosePage(controller.StackManager.Stacked[i].Page);
            }

            controller.StackManager.Stacked.RemoveRange(controller.StackManager.Stacked.Count - level, level);

            controller.StackManager.Stacked.Add(new PageStack(descriptor, controller.CreatePage(descriptor.Type)));
        }

        // ------------------------------------------------------------
        // Navigation
        // ------------------------------------------------------------

        // TODO Controllerに処理を閉じ込める
        // TODO Controllerに状態を閉じ込める
        // TODO StrategyにControllerのみを渡す

        private void InternalNavigate(
            INavigationController controller,
            Action<INavigationController> updateStack)
        {
            if (controller.Provider.IsAsync)
            {
                controller.Provider.BeginInvoke(() => InternalNavigate(controller, updateStack));
                return;
            }

            controller.Pipeline?.OnPreProcess(controller.PluginContext);

            controller.ProcessNavigatedFrom();

            var args = new NavigationEventArgs(controller.NavigationContext);

            NavigatedFrom?.Invoke(this, args);

            // TODO Strategy化
            updateStack(controller);

            NavigatedTo?.Invoke(this, args);

            controller.ProcessNavigatedTo();

            controller.Pipeline?.OnPostProcess(controller.PluginContext);
        }

        // ------------------------------------------------------------
        // Helper
        // ------------------------------------------------------------

        private bool ConfirmNavigation(INavigationController controller)
        {
            var page = CurrentPage;
            if (page != null)
            {
                var target = controller.Provider.ResolveTarget(page);
                var confirm = target as IConfirmRequest;
                if (confirm != null)
                {
                    var cancel = confirm.NavigationConfirm(controller.NavigationContext);
                    if (cancel)
                    {
                        return false;
                    }
                }
            }

            var handler = Confirm;
            if (handler != null)
            {
                var args = new ConfirmEventArgs(controller.NavigationContext);
                handler(this, args);
                if (args.Cancel)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
