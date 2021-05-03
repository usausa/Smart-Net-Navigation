namespace Smart.Navigation.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    public sealed class GroupPushStrategy : INavigationStrategy
    {
        private readonly object id;

        [AllowNull]
        private ViewDescriptor descriptor;

        private List<int>? groups;

        private bool exist;

        private ViewStackInfo? activateStackInfo;

        private ViewStackInfo? deactivateStackInfo;

        private List<int> PreparedGroups
        {
            get
            {
                groups ??= new List<int>();
                return groups;
            }
        }

        public GroupPushStrategy(object id)
        {
            this.id = id;
        }

        public StrategyResult? Initialize(INavigationController controller)
        {
            descriptor = controller.ViewMapper.FindDescriptor(id);

            var group = descriptor.Type.GetCustomAttribute<GroupAttribute>();
            if (group is null)
            {
                throw new InvalidOperationException($"View is not grouped. id=[{id}]");
            }

            var current = -1;
            for (var i = 0; i < controller.ViewStack.Count; i++)
            {
                var groupOfStack = controller.ViewStack[i].Descriptor.Type.GetCustomAttribute<GroupAttribute>();
                if ((groupOfStack is not null) && Equals(group.Id, groupOfStack.Id))
                {
                    PreparedGroups.Add(i);

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

                // Deactivate top & Active current
                exist = true;
                deactivateStackInfo = controller.ViewStack[^1];
                activateStackInfo = controller.ViewStack[PreparedGroups[^1]];

                return new StrategyResult(activateStackInfo.Descriptor.Id, NavigationAttributes.Restore);
            }

            if (controller.ViewStack.Count > 0)
            {
                deactivateStackInfo = controller.ViewStack[^1];
            }

            return new StrategyResult(id, NavigationAttributes.Stacked);
        }

        public object ResolveToView(INavigationController controller)
        {
            if (!exist)
            {
                return controller.CreateView(descriptor.Type);
            }

            return controller.ViewStack[PreparedGroups[^1]];
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
            if (groups is not null)
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
            if (activateStackInfo is not null)
            {
                controller.ActivateView(activateStackInfo.View, activateStackInfo.RestoreParameter);
                activateStackInfo.RestoreParameter = null;
            }

            // Deactivate old
            if (deactivateStackInfo is not null)
            {
                deactivateStackInfo.RestoreParameter = controller.DeactivateView(deactivateStackInfo.View);
            }
        }
    }
}
