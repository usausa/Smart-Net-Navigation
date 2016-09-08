namespace Smart.Navigation.Plugins.Parameter
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ParameterAttribute : Attribute
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
        public ParameterAttribute()
            : this(Direction.Both)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public ParameterAttribute(string name)
            : this(Direction.Both, name)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="direction"></param>
        public ParameterAttribute(Direction direction)
        {
            Direction = direction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="name"></param>
        public ParameterAttribute(Direction direction, string name)
        {
            Direction = direction;
            Name = name;
        }
    }
}
