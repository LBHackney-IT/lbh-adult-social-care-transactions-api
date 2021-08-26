using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Extensions
{
    public static class EnumExtensions
    {
        // Note that we never need to expire these cache items, so we just use ConcurrentDictionary rather than MemoryCache
        /*private static readonly ConcurrentDictionary<string, string> _displayNameCache = new ConcurrentDictionary<string, string>();

        public static string DisplayName(this Enum value)
        {
            var key = $"{value.GetType().FullName}.{value}";

            var displayName = _displayNameCache.GetOrAdd(key, x =>
            {
                var name = (DescriptionAttribute[]) value
                    .GetType()
                    .GetTypeInfo()
                    .GetField(value.ToString())
                    ?.GetCustomAttributes(typeof(DescriptionAttribute), false);

                return name != null && name.Length > 0 ? name[0].Description : value.ToString();
            });

            return displayName;
        }*/

        public static string ToDescription(this Enum enumeration)
        {
            try
            {
                var attribute = GetText<DescriptionAttribute>(enumeration);

                return attribute.Description;
            }
            catch (ArgumentException)
            {
                return nameof(enumeration);
            }
        }

        public static string GetDisplayName(this Enum enumeration)
        {
            try
            {
                var attribute = GetText<DisplayAttribute>(enumeration);

                return attribute.GetName();
            }
            catch (ArgumentException)
            {
                return nameof(enumeration);
            }
        }

        public static T GetText<T>(Enum enumeration) where T : Attribute
        {
            var type = enumeration.GetType();

            var memberInfo = type.GetMember(enumeration.ToString());

            if (!memberInfo.Any())
                throw new ArgumentException($"No public members for the argument '{enumeration}'.");

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            if (attributes == null || attributes.Length != 1)
                throw new ArgumentException($"Can't find an attribute matching '{typeof(T).Name}' for the argument '{enumeration}'");

            return attributes.Single() as T;
        }

        public static string ConvertEnumToString(this Enum enumeration)
        {
            return enumeration.ToString();
        }

        public static bool EnumIsDefined<T>(this T enumeration, string status) where T : Enum
        {
            return Enum.IsDefined(enumeration.GetType(), status);
        }

        public static T ConvertStringToEnum<T>(this T enumeration, string status) where T : Enum
        {
            return (T) Enum.Parse(enumeration.GetType(), status, true);
        }

        public static string ConvertNumberToEnumName<T>(this T enumeration, int value) where T : Enum
        {
            return Enum.GetName(typeof(T), value);
        }

        public static T ConvertNumberToEnum<T>(this T enumeration, int value) where T : Enum
        {
            var name = Enum.GetName(typeof(T), value);
            return enumeration.ConvertStringToEnum(name);
        }

        public static int Count<T>(this T enumeration) where T : Enum
        {
            return Enum.GetNames(typeof(T)).Length;
        }
    }
}
