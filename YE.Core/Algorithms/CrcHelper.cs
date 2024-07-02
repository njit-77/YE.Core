using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace YE.Core.Algorithms
{
    /// <summary>
    /// CRC-算法辅助类
    /// <para>
    /// 常用查表法和计算法。
    /// </para>
    /// </summary>
    public class CrcHelper
    {

        #region 计算法

        /// <summary>
        /// CRC-16校验算法[计算法]
        /// </summary>
        /// <example>
        /// <code lang="cs">
        /// <![CDATA[
        /// GetCrc16(data, 0x1021, true) -- CRC-16/MCRF4XX
        /// GetCrc16(data, 0x1021, false) -- CRC-16/XMODEM
        /// GetCrc16(data, 0x8005, true) -- CRC-16/MODBUS
        /// GetCrc16(data, 0x8005, false) -- CRC-16/BUYPASS
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="data">数据</param>
        /// <param name="ploy">多项式</param>
        /// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
        /// <returns>CRC结果</returns>
        public static UInt16 GetCrc16(byte[] data, UInt16 ploy, bool isReverse)
        {
            UInt16 crc = 0xFFFF;
            if (isReverse)
            {
                /// 逆序多项式
                BitVector32 bits = new BitVector32((Int32)ploy);
                BitVector32 reverse_bits = new BitVector32();
                for (int i = 0; i < 32; i++)
                {
                    reverse_bits[1 << (31 - i)] = bits[1 << i];
                }
                ploy = (UInt16)((reverse_bits.Data >> 16) & 0xffff);


                crc = 0xFFFF;
                for (UInt16 i = 0; i < data.Length; i++)
                {
                    crc ^= data[i];
                    for (UInt16 j = 0; j < 8; j++)
                    {
                        if ((crc & 0x1) != 0)
                        {
                            crc = (UInt16)((crc >> 1) ^ ploy);
                        }
                        else
                        {
                            crc = (UInt16)(crc >> 1);
                        }
                    }
                }
            }
            else
            {
                crc ^= crc;
                for (UInt16 i = 0; i < data.Length; i++)
                {
                    crc ^= (UInt16)(data[i] << 8);
                    for (UInt16 j = 0; j < 8; j++)
                    {
                        if ((crc & 0x8000) != 0)
                        {
                            crc = (UInt16)((crc << 1) ^ ploy);
                        }
                        else
                        {
                            crc = (UInt16)(crc << 1);
                        }
                    }
                }
            }
            return crc;
        }

        /// <summary>
        /// CRC-32校验算法[计算法]
        /// </summary>
        /// <example>
        /// <code lang="cs">
        /// <![CDATA[
        /// GetCrc32(data, 0x04C11DB7, true) -- CRC-32
        /// GetCrc32(data, 0x04C11DB7, false) -- CRC-32/POSIX
        /// GetCrc32(data, 0x1EDC6F41, true) -- CRC-32C
        /// ]]>
        /// </code>>
        /// </example>
        /// <param name="data">数据</param>
        /// <param name="ploy">多项式</param>
        /// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
        /// <returns>CRC结果</returns>
        public static UInt32 GetCrc32(byte[] data, UInt32 ploy, bool isReverse)
        {
            UInt32 crc = 0xFFFFFFFF;
            if (isReverse)
            {
                /// 逆序多项式
                BitVector32 bits = new BitVector32((Int32)ploy);
                BitVector32 reverse_bits = new BitVector32();
                for (int i = 0; i < 32; i++)
                {
                    reverse_bits[1 << (31 - i)] = bits[1 << i];
                }
                ploy = (UInt32)reverse_bits.Data;

                crc = 0xFFFFFFFF;
                for (UInt32 i = 0; i < data.Length; i++)
                {
                    crc ^= data[i];
                    for (UInt32 j = 0; j < 8; j++)
                    {
                        if ((crc & 0x1) != 0)
                        {
                            crc = (UInt32)((crc >> 1) ^ ploy);
                        }
                        else
                        {
                            crc = (UInt32)(crc >> 1);
                        }
                    }
                }
            }
            else
            {
                crc ^= crc;
                for (UInt32 i = 0; i < data.Length; i++)
                {
                    crc ^= (UInt32)(data[i] << 24);
                    for (UInt32 j = 0; j < 8; j++)
                    {
                        if ((crc & 0x80000000) != 0)
                        {
                            crc = (UInt32)((crc << 1) ^ ploy);
                        }
                        else
                        {
                            crc = (UInt32)(crc << 1);
                        }
                    }
                }
            }
            return crc ^ 0xFFFFFFFF;
        }

        #endregion


        #region 查表法

        /// <summary>
        /// CRC16-生成表方法
        /// </summary>
        /// <param name="poly">多项式</param>
        /// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
        /// <returns>表</returns>
        public static UInt16[] GenerateCrc16Table(UInt16 poly, bool isReverse)
        {
            UInt16[] crc_table = new UInt16[256];

            if (isReverse)
            {
                /// 逆序多项式
                BitVector32 bits = new BitVector32((Int32)poly);
                BitVector32 reverse_bits = new BitVector32();
                for (int i = 0; i < 32; i++)
                {
                    reverse_bits[1 << (31 - i)] = bits[1 << i];
                }
                poly = (UInt16)((reverse_bits.Data >> 16) & 0xffff);
            }

            Parallel.For(0, 256, number =>
            {
                UInt16 crc;

                if (isReverse)
                {
                    crc = (UInt16)number;
                    for (UInt16 i = 0; i < 8; i++)
                    {
                        if ((crc & 0x1) != 0)
                        {
                            crc = (UInt16)((crc >> 1) ^ poly);
                        }
                        else
                        {
                            crc = (UInt16)(crc >> 1);
                        }
                    }
                }
                else
                {
                    crc = (UInt16)(number << 8);
                    for (UInt16 i = 0; i < 8; i++)
                    {
                        if ((crc & 0x8000) != 0)
                        {
                            crc = (UInt16)((crc << 1) ^ poly);
                        }
                        else
                        {
                            crc = (UInt16)(crc << 1);
                        }
                    }
                }

                crc_table[number] = crc;
            });

            return crc_table;
        }

        /// <summary>
        /// CRC32-生成表方法
        /// </summary>
        /// <param name="poly">多项式</param>
        /// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
        /// <returns>表</returns>
        public static UInt32[] GenerateCrc32Table(UInt32 poly, bool isReverse)
        {
            UInt32[] crc_table = new UInt32[256];

            if (isReverse)
            {
                /// 逆序多项式
                BitVector32 bits = new BitVector32((Int32)poly);
                BitVector32 reverse_bits = new BitVector32();
                for (int i = 0; i < 32; i++)
                {
                    reverse_bits[1 << (31 - i)] = bits[1 << i];
                }
                poly = (UInt32)reverse_bits.Data;
            }

            Parallel.For(0, 256, number =>
            {
                UInt32 crc;

                if (isReverse)
                {
                    crc = (UInt32)number;
                    for (UInt32 i = 0; i < 8; i++)
                    {
                        if ((crc & 0x1) != 0)
                        {
                            crc = (UInt32)((crc >> 1) ^ poly);
                        }
                        else
                        {
                            crc = (UInt32)(crc >> 1);
                        }
                    }
                }
                else
                {
                    crc = (UInt32)(number << 24);
                    for (UInt32 i = 0; i < 8; i++)
                    {
                        if ((crc & 0x80000000) != 0)
                        {
                            crc = (UInt32)((crc << 1) ^ poly);
                        }
                        else
                        {
                            crc = (UInt32)(crc << 1);
                        }
                    }
                }

                crc_table[number] = crc;
            });

            return crc_table;
        }

        /// <summary>
        /// CRC-16校验算法[查表法]
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="crcTable">表</param>
        /// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
        /// <returns>CRC结果</returns>
        public static UInt16 GetCrc16WithTable(byte[] data, UInt16[] crcTable, bool isReverse)
        {
            UInt16 crc = 0xFFFF;

            if (isReverse)
            {
                for (UInt16 i = 0; i < data.Length; i++)
                {
                    crc = (UInt16)((crc >> 8) ^ crcTable[(crc ^ data[i]) & 0xFF]);
                }
            }
            else
            {
                crc ^= crc;
                for (UInt16 i = 0; i < data.Length; i++)
                {
                    crc = (UInt16)((crc << 8) ^ crcTable[(crc >> 8 ^ data[i]) & 0xFF]);
                }
            }

            return crc;
        }

        /// <summary>
        /// CRC-32校验算法[查表法]
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="crcTable">表</param>
        /// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
        /// <returns>CRC结果</returns>
        public static UInt32 GetCrc32WithTable(byte[] data, UInt32[] crcTable, bool isReverse)
        {
            UInt32 crc = 0xFFFFFFFF;

            if (isReverse)
            {
                for (UInt16 i = 0; i < data.Length; i++)
                {
                    crc = (UInt32)((crc >> 8) ^ crcTable[(crc ^ data[i]) & 0xFF]);
                }
            }
            else
            {
                crc ^= crc;
                for (UInt16 i = 0; i < data.Length; i++)
                {
                    crc = (UInt32)((crc << 8) ^ crcTable[(crc >> 24 ^ data[i]) & 0xFF]);
                }
            }

            return crc ^ 0xFFFFFFFF;
        }

        #endregion


    }
}
