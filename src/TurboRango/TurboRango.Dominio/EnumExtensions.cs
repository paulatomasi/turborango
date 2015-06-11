using System.ComponentModel;

namespace TurboRango.Dominio.Utils
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T value)
        {
            var type = typeof(T);
            var memInfo = type.GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}