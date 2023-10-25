using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Final.Domain.Extention;

public static class EnumExtention
{
    public static string GetDisplayName(this System.Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? "Неопределенный";
    }
}
