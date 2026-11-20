namespace Smart.Navigation.Strategies;

using System.Collections.Concurrent;
using System.Reflection;

internal static class GroupAttributeCache
{
    private static readonly ConcurrentDictionary<Type, GroupAttribute?> Attributes = new();

    public static GroupAttribute? Resolve(Type type) =>
        Attributes.GetOrAdd(type, static x => x.GetCustomAttribute<GroupAttribute>());
}
