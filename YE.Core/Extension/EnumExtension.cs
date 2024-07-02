using System;
using System.ComponentModel;
using System.Reflection;

namespace YE.Core.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取Enum类型的Description信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            string result = value.ToString();

            var field = value.GetType().GetField(result);

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

            if (attribute != null)
            {
                return attribute.Description;
            }
            return result;
        }
    }
}
