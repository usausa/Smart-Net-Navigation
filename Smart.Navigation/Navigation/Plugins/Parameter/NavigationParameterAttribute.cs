namespace Smart.Navigation.Plugins.Parameter
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class NavigationParameterAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public Direction Direction { get; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///
        /// </summary>
        public NavigationParameterAttribute()
            : this(Direction.Both)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public NavigationParameterAttribute(string name)
            : this(Direction.Both, name)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="direction"></param>
        public NavigationParameterAttribute(Direction direction)
        {
            Direction = direction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="name"></param>
        public NavigationParameterAttribute(Direction direction, string name)
        {
            Direction = direction;
            Name = name;
        }
    }
}
