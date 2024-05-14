using System;

namespace YE.Core.Extension
{
    public static class EnumExtension
    {
        public static string GetDescriptionByEnum<T>(this T value) where T : Enum
        {
            string result = value.ToString();

            var fi = typeof(T).GetField(result);

            var attributes = (string[])fi.GetCustomAttributes(typeof(string), true);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0];
            }
            return result;
        }
    }
}
