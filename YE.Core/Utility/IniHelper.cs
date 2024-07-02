using System.Runtime.InteropServices;
using System.Text;

namespace YE.Core.Utility
{
    /// <summary>
    /// ini文件操作类
    /// 1、支持bool、int、float、double、string类型;
    /// 2、float类型有效位数7位;double类型有效位数15位;
    /// 3、string类型有效位数512位;
    /// 4、当string类型为多语言且出现乱码时，需提前创建ini文件且格式为UTF16 LE;
    /// </summary>
    public class IniHelper
    {

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <example>
        /// <code lang="cs">
        /// <![CDATA[
        /// string file_path = "./test.ini";
        /// IniHelper.WriteValue("BOOL", "test_1", false, file_path);
        /// IniHelper.WriteValue("INT", "test_3", int.MinValue, file_path);
        /// IniHelper.WriteValue("FLOAT", "test_6", -1234.567, file_path);
        /// IniHelper.WriteValue("DOUBLE", "test_9", -123456789.987654, file_path);
        /// IniHelper.WriteValue("STRING", "test_14", "안녕하세요", file_path);
        /// ]]>
        /// </code>
        /// </example>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="filePath">ini文件</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteValue<T>(string section, string key, T value, string filePath)
        {
            return WritePrivateProfileString(section, key, value.ToString(), filePath) != 0;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <example>
        /// <code lang="cs">
        /// <![CDATA[
        /// string file_path = "./test.ini";
        /// bool test_1 = IniHelper.ReadValue<bool>("BOOL", "test_1", file_path);
        /// int test_3 = IniHelper.ReadValue<int>("INT", "test_3", file_path)
        /// float test_6 = IniHelper.ReadValue<float>("FLOAT", "test_6", file_path);
        /// double test_9 = IniHelper.ReadValue<double>("DOUBLE", "test_9", file_path);
        /// string test_14 = IniHelper.ReadValue<string>("STRING", "test_14", file_path);
        /// ]]>
        /// </code>
        /// </example>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="filePath">ini文件</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T ReadValue<T>(string section, string key, string filePath, T defaultValue = default(T))
        {
            int count = 512;

            var sb = new StringBuilder(count);
            GetPrivateProfileString(section, key, "", sb, count, filePath);

            if (typeof(T) == typeof(bool))
            {
                if (bool.TryParse(sb.ToString(), out bool result))
                {
                    return (T)(object)result;
                }
            }
            else if (typeof(T) == typeof(int))
            {
                if (int.TryParse(sb.ToString(), out int result))
                {
                    return (T)(object)result;
                }
            }
            else if (typeof(T) == typeof(float))
            {
                if (float.TryParse(sb.ToString(), out float result))
                {
                    return (T)(object)result;
                }
            }
            else if (typeof(T) == typeof(double))
            {
                if (double.TryParse(sb.ToString(), out double result))
                {
                    return (T)(object)result;
                }
            }
            else if (typeof(T) == typeof(string))
            {
                return (T)(object)sb.ToString();
            }

            return defaultValue;
        }
    }
}
