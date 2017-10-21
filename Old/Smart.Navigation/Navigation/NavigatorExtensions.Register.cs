namespace Smart.Navigation
{
    using System;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static partial class NavigatorExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void AutoRegister(this Navigator navigator, Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            foreach (var type in assembly.ExportedTypes)
            {
                foreach (var attr in type.GetTypeInfo().GetCustomAttributes<PageDescriptorAttribute>())
                {
                    navigator.Register(attr.CreateDescriptor(type));
                }
            }
        }

        public static void Register(this Navigator navigator, object id, Type type)
        {
            navigator.Register(new PageDescriptor(id, null, type));
        }

        public static void Register(this Navigator navigator, object id, object domain, Type type)
        {
            navigator.Register(new PageDescriptor(id, domain, type));
        }
    }
}
