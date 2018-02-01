namespace Smart.Navigation
{
    using System;
    using System.Collections.Generic;
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
                foreach (var attr in type.GetTypeInfo().GetCustomAttributes<ViewAttribute>())
                {
                    navigator.Register(attr.Id ?? type, type);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void Register(this Navigator navigator, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            navigator.Register(type, type);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void Register(this Navigator navigator, IEnumerable<Type> types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            foreach (var type in types)
            {
                navigator.Register(type, type);
            }
        }
    }
}
