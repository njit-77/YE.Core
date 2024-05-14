using System.Text;
using YE.Core.Algorithms;

namespace YE.Core.Test.Algorithms
{
    public class CrcHelperTest
    {
        [Fact]
        public void Test1()
        {
            byte[] data = Encoding.Unicode.GetBytes("今天天气很好");

            Assert.Equal(CrcHelper.GetCrc16(data, 0x1021, false), (UInt16)0x5DC1);
            Assert.Equal(CrcHelper.GetCrc16(data, 0x8005, true), (UInt16)0xD63B);
            Assert.Equal(CrcHelper.GetCrc32(data, 0x04C11DB7, true), (UInt32)0x7B1B0DAA);
        }
    }
}
