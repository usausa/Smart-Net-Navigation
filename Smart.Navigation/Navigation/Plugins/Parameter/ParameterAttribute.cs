namespace Smart.Navigation.Plugins.Parameter
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParameterAttribute : Attribute
    {
        public Direction Direction { get; }

        public string Name { get; }

        public ParameterAttribute()
            : this(Direction.Both)
        {
        }

        public ParameterAttribute(string name)
            : this(Direction.Both, name)
        {
        }

        public ParameterAttribute(Direction direction)
        {
            Direction = direction;
        }

        public ParameterAttribute(Direction direction, string name)
        {
            Direction = direction;
            Name = name;
        }
    }
}
