namespace Smart.Navigation.Plugins
{
    using System;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAttributeMember<out T>
        where T : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        string Name { get; }

        /// <summary>
        ///
        /// </summary>
        Type MemberType { get; }

        /// <summary>
        ///
        /// </summary>
        T Attribute { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        object GetValue(object target);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        void SetValue(object target, object value);
    }
}
