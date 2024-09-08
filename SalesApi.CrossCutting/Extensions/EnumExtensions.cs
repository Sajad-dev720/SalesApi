using System.ComponentModel;

namespace SalesApi.CrossCutting.Extensions;

public static class EnumExtensions
{
    public static string? GetEnumDescription(this Enum value)
    {
        if (value == null)
        {
            return string.Empty;
        }
        var description = value.GetAttribute<DescriptionAttribute>();
        return description?.Description;
    }

    private static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name == null)
            return default;
        return type.GetField(name)!.GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault()!;
    }
}
