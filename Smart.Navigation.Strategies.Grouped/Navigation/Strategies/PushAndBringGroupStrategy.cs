namespace Smart.Navigation.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class PushAndBringGroupStrategy : INavigationStrategy
    {
        private readonly object id;

        private PageDescriptor descriptor;

        private List<int> groups;

        private bool exist;

        private PageStackInfo activeStackInfo;

        private PageStackInfo deactiveStackInfo;

        public PushAndBringGroupStrategy(object id)
        {
            this.id = id;
        }

        public StragtegyResult Initialize(INavigationController controller)
        {
            if (!controller.Descriptors.TryGetValue(id, out descriptor))
            {
                throw new InvalidOperationException($"Page id is not found in descriptors. id=[{id}]");
            }

            var current = -1;
            var group = descriptor.Type.GetCustomAttribute<GroupAttribute>();
            if (group != null)
            {
                for (var i = 0; i < controller.PageStack.Count; i++)
                {
                    var groupOfStack = controller.PageStack[i].Descriptor.Type.GetCustomAttribute<GroupAttribute>();
                    if ((groupOfStack != null) && Equals(group.Id, groupOfStack.Id))
                    {
                        if (groups == null)
                        {
                            groups = new List<int>();
                        }

                        groups.Add(i);

                        if (Equals(controller.PageStack[i].Descriptor.Id, id))
                        {
                            current = i;
                        }
                    }
                }
            }

            // exist ?
            if (current != -1)
            {
                // and last ?
                if (controller.PageStack.Count - 1 == current)
                {
                    return null;
                }

                // Deactive top & Active current
                exist = true;
                deactiveStackInfo = controller.PageStack[controller.PageStack.Count - 1];
                activeStackInfo = controller.PageStack[current];

                return new StragtegyResult(activeStackInfo.Descriptor.Id, NavigationAttributes.Restore);
            }

            if (controller.PageStack.Count > 0)
            {
                deactiveStackInfo = controller.PageStack[controller.PageStack.Count - 1];
            }

            return new StragtegyResult(id, NavigationAttributes.Stacked);
        }

        public object ResolveToPage(INavigationController controller)
        {
            if (!exist)
            {
                return controller.CreatePage(descriptor.Type);
            }

            return controller.PageStack[groups[groups.Count - 1]];
        }

        public void UpdateStack(INavigationController controller, object toPage)
        {
            // Stack new
            if (!exist)
            {
                controller.PageStack.Add(new PageStackInfo(descriptor, toPage));

                controller.OpenPage(toPage);
            }

            // Replace stack
            if (groups != null)
            {
                var count = controller.PageStack.Count - (exist ? 0 : 1);

                var temp = new PageStackInfo[count];
                controller.PageStack.CopyTo(0, temp, 0, count);

                var index = 0;
                for (var i = 0; i < count - groups.Count; i++)
                {
                    while ((index < groups.Count) && (groups[index] <= index + i))
                    {
                        index++;
                    }

                    controller.PageStack[i] = temp[index + i];
                }

                var offset = count - groups.Count;
                for (var i = 0; i < groups.Count; i++)
                {
                    controller.PageStack[offset + i] = temp[groups[i]];
                }
            }

            // Activate restored
            if (activeStackInfo != null)
            {
                controller.ActivePage(activeStackInfo.Page, activeStackInfo.RestoreParameter);
                activeStackInfo.RestoreParameter = null;
            }

            // Deactive old
            if (deactiveStackInfo != null)
            {
                deactiveStackInfo.RestoreParameter = controller.DeactivePage(deactiveStackInfo.Page);
            }
        }
    }
}
