namespace Smart.Navigation.Components;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

internal static class EffectFactoryHelper
{
    public static object Create([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type, Func<Type, object?>? resolver)
    {
        foreach (var constructor in type.GetConstructors().OrderByDescending(static x => x.GetParameters().Length))
        {
            var arguments = ResolveArguments(constructor.GetParameters(), resolver);
            if (arguments is not null)
            {
                return constructor.Invoke(arguments);
            }
        }

        throw new InvalidOperationException($"Constructor parameter unresolved. type=[{type.Name}]");
    }

    private static object?[]? ResolveArguments(ParameterInfo[] parameters, Func<Type, object?>? resolver)
    {
        var arguments = new object?[parameters.Length];
        for (var i = 0; i < parameters.Length; i++)
        {
            var parameter = parameters[i];
            var argument = resolver?.Invoke(parameter.ParameterType);
            if (argument is not null)
            {
                arguments[i] = argument;
            }
            else if (parameter.HasDefaultValue)
            {
                arguments[i] = parameter.DefaultValue;
            }
            else
            {
                return null;
            }
        }

        return arguments;
    }
}
