﻿namespace Smart.Navigation.Strategies
{
    using System;

    using Smart.Navigation.Descriptors;

    public sealed class PushStrategy : INavigationStrategy
    {
        private readonly object id;

        private ViewDescriptor descriptor;

        public PushStrategy(object id)
        {
            this.id = id;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            descriptor = controller.ViewMapper.FindDescriptor(id);

            return new StragtegyResult(id, NavigationAttributes.Stacked);
        }

        public object ResolveToView(INavigationController controller)
        {
            return controller.CreateView(descriptor.Type);
        }

        public void UpdateStack(INavigationController controller, object toView)
        {
            // Stack new
            controller.ViewStack.Add(new ViewStackInfo(descriptor, toView));

            controller.OpenView(toView);

            // Deactive old
            var count = controller.ViewStack.Count;
            if (count > 1)
            {
                var index = count - 2;

                controller.ViewStack[index].RestoreParameter = controller.DeactiveView(controller.ViewStack[index].View);
            }
        }
    }
}
