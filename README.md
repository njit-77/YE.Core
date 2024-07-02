# YE.Core
YE.Core是一个基础类库，包括开发常用功能。



## CrcHelper

#### CRC-算法辅助类，分常用查表法和计算法。

###### 计算法

```c#
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
```

###### 查表法

```c#
/// <summary>
/// CRC16-生成表方法
/// </summary>
/// <param name="poly">多项式</param>
/// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
/// <returns>表</returns>
public static UInt16[] GenerateCrc16Table(UInt16 poly, bool isReverse)
    
    
/// <summary>
/// CRC32-生成表方法
/// </summary>
/// <param name="poly">多项式</param>
/// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
/// <returns>表</returns>
public static UInt32[] GenerateCrc32Table(UInt32 poly, bool isReverse)

    
/// <summary>
/// CRC-16校验算法[查表法]
/// </summary>
/// <param name="data">数据</param>
/// <param name="crcTable">表</param>
/// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
/// <returns>CRC结果</returns>
public static UInt16 GetCrc16WithTable(byte[] data, UInt16[] crcTable, bool isReverse)

    
/// <summary>
/// CRC-32校验算法[查表法]
/// </summary>
/// <param name="data">数据</param>
/// <param name="crcTable">表</param>
/// <param name="isReverse">true:逆序 低位在左，高位在右，false:正序 高位在左，低位在右</param>
/// <returns>CRC结果</returns>
public static UInt32 GetCrc32WithTable(byte[] data, UInt32[] crcTable, bool isReverse)
```



## EnumExtension

#### 获取Enum类型的Description信息

```c#
/// <summary>
/// 获取Enum类型的Description信息
/// </summary>
/// <param name="value"></param>
/// <returns></returns>
public static string GetDescription(this Enum value)
```



## IniHelper

#### ini文件操作类

- 支持bool、int、float、double、string类型;
- float类型有效位数7位;double类型有效位数15位;
- string类型有效位数512位;
- 当string类型为多语言且出现乱码时，需提前创建ini文件且格式为UTF16 LE;

```c#
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
```



## ProducerConsumer

#### 简单生产者-消费者模式

```c#
var producerConsumer = new ProducerConsumer<int>(t =>
{
    Task.Run(() =>
    {
        Console.WriteLine($"Hello YE.Core, This is from {t}");
    });
});

producerConsumer.Add(10);
```

