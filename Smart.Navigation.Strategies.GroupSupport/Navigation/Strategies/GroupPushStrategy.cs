namespace Smart.Navigation.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public sealed class GroupPushStrategy : INavigationStrategy
    {
        private readonly object id;

        private ViewDescriptor descriptor;

        private List<int> groups;

        private bool exist;

        private ViewStackInfo activeStackInfo;

        private ViewStackInfo deactiveStackInfo;

        public GroupPushStrategy(object id)
        {
            this.id = id;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            descriptor = controller.ViewMapper.FindDescriptor(id);

            var group = descriptor.Type.GetCustomAttribute<GroupAttribute>();
            if (group == null)
            {
                throw new InvalidOperationException($"View is not grouped. id=[{id}]");
            }

            var current = -1;
            for (var i = 0; i < controller.ViewStack.Count; i++)
            {
                var groupOfStack = controller.ViewStack[i].Descriptor.Type.GetCustomAttribute<GroupAttribute>();
                if ((groupOfStack != null) && Equals(group.Id, groupOfStack.Id))
                {
                    if (groups == null)
                    {
                        groups = new List<int>();
                    }

                    groups.Add(i);

                    if (Equals(controller.ViewStack[i].Descriptor.Id, id))
                    {
                        current = i;
                    }
                }
            }

            // exist ?
            if (current != -1)
            {
                // and last ?
                if (controller.ViewStack.Count - 1 == current)
                {
                    return null;
                }

                // Deactive top & Active current
                exist = true;
                deactiveStackInfo = controller.ViewStack[controller.ViewStack.Count - 1];
                activeStackInfo = controller.ViewStack[groups[groups.Count - 1]];

                return new StragtegyResult(activeStackInfo.Descriptor.Id, NavigationAttributes.Restore);
            }

            if (controller.ViewStack.Count > 0)
            {
                deactiveStackInfo = controller.ViewStack[controller.ViewStack.Count - 1];
            }

            return new StragtegyResult(id, NavigationAttributes.Stacked);
        }

        public object ResolveToView(INavigationController controller)
        {
            if (!exist)
            {
                return controller.CreateView(descriptor.Type);
            }

            return controller.ViewStack[groups[groups.Count - 1]];
        }

        public void UpdateStack(INavigationController controller, object toView)
        {
            // Stack new
            if (!exist)
            {
                controller.ViewStack.Add(new ViewStackInfo(descriptor, toView));

                controller.OpenView(toView);
            }

            // Replace stack
            if (groups != null)
            {
                var count = controller.ViewStack.Count - (exist ? 0 : 1);

                var temp = new ViewStackInfo[count];
                controller.ViewStack.CopyTo(0, temp, 0, count);

                var index = 0;
                for (var i = 0; i < count - groups.Count; i++)
                {
                    while ((index < groups.Count) && (groups[index] <= index + i))
                    {
                        index++;
                    }

                    controller.ViewStack[i] = temp[index + i];
                }

                var offset = count - groups.Count;
                for (var i = 0; i < groups.Count; i++)
                {
                    controller.ViewStack[offset + i] = temp[groups[i]];
                }
            }

            // Activate restored
            if (activeStackInfo != null)
            {
                controller.ActiveView(activeStackInfo.View, activeStackInfo.RestoreParameter);
                activeStackInfo.RestoreParameter = null;
            }

            // Deactive old
            if (deactiveStackInfo != null)
            {
                deactiveStackInfo.RestoreParameter = controller.DeactiveView(deactiveStackInfo.View);
            }
        }
    }
}
