using System.ComponentModel;
using YE.Core.Extension;

namespace YE.Core.Test.Extension
{
    public class EnumExtensionTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("All", LogLevel.All.GetDescription());
            Assert.Equal("Debug", LogLevel.Debug.GetDescription());
            Assert.Equal("Information", LogLevel.Info.GetDescription());
            Assert.Equal("Warn", LogLevel.Warning.GetDescription());
            Assert.Equal("Error", LogLevel.Error.GetDescription());
            Assert.Equal("Fatal", LogLevel.Fatal.GetDescription());
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
            [Description("Information")]
            Info = 2,

            /// <summary>警告</summary>
            [Description("Warn")]
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
