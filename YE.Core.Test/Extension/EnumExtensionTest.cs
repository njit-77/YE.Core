using System.ComponentModel;
using YE.Core.Extension;

namespace YE.Core.Test.Extension
{
    public class EnumExtensionTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("All", LogLevel.All.GetDescriptionByEnum());
            Assert.Equal("Debug", LogLevel.Debug.GetDescriptionByEnum());
            Assert.Equal("Info", LogLevel.Info.GetDescriptionByEnum());
            Assert.Equal("Warning", LogLevel.Warning.GetDescriptionByEnum());
            Assert.Equal("Error", LogLevel.Error.GetDescriptionByEnum());
            Assert.Equal("Fatal", LogLevel.Fatal.GetDescriptionByEnum());
        }

        public enum LogLevel : byte
        {
            /// <summary>所有</summary>
            [Description("All")]
            All = 0,

            /// <summary>调试</summary>
            [Description("Debug")]
            Debug = 1,

            /// <summary>普通</summary>
            [Description("Info")]
            Info = 2,

            /// <summary>警告</summary>
            [Description("Warning")]
            Warning = 3,

            /// <summary>错误</summary>
            [Description("Error")]
            Error = 4,

            /// <summary>严重错误</summary>
            [Description("Fatal")]
            Fatal = 5,

            /// <summary>关闭</summary>
            Off = byte.MaxValue,
        }
    }
}
