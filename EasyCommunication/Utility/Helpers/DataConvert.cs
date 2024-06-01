using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EasyCommunication.Common.Helpers
{
    /// <summary>
    /// 数据转换
    /// </summary>
    public static class DataConvert
    {
        /// <summary>
        /// 字节数组转16进制字符
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static string ByteArrayToString(this byte[] byteArray)
        {
            return string.Join(" ", byteArray.Select(t => t.ToString("X2")));
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] StringToByteArray(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException("请传入有效的参数");

            //string[] array = str.Trim().Split(' ');
            //// 添加数量
            //UInt16 sourceInt16 = Convert.ToUInt16(string.Format("0x{0}", array.Length), 16);
            //byte[] sourceByte = BitConverter.GetBytes(sourceInt16);
            //// 添加数据
            //string c = Convert.ToInt32(string.Join("", array), 2).ToString("X2");
            //UInt16 cInt16 = Convert.ToUInt16(string.Format("0x{0}", c), 16);

            //return [];
            return str.Split(' ').Where(t => t?.Length != 0).Select(t => Convert.ToByte(t, 16)).ToArray();

            //if (strict)
            //{
            //    string[] array = str.Trim().Split(' ');
            //    // 添加数量
            //    UInt16 sourceInt16 = Convert.ToUInt16(string.Format("0x{0}", array.Length), 16);
            //    byte[] sourceByte = BitConverter.GetBytes(sourceInt16);
            //    // 添加数据
            //    string c = Convert.ToInt32(string.Join("", array), 2).ToString("X2");
            //    UInt16 cInt16 = Convert.ToUInt16(string.Format("0x{0}", c), 16);

            //    return [];
            //    return str.Split(' ').Where(t => t?.Length != 0).Select(t => Convert.ToByte(t, 16)).ToArray();
            //}
            //else
            //{
            //    str = str.Trim().Replace(" ", "");
            //    var list = new List<byte>();
            //    for (int i = 0; i < str.Length; i++)
            //    {
            //        var string16 = str[i].ToString() + str[++i].ToString();
            //        list.Add(Convert.ToByte(string16, 16));
            //    }
            //    return list.ToArray();
            //}
        }



        /// <summary>
        /// 字节数组转换成Ascii字节数组
        /// 如：00 01 => 30 31
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ByteArrayToAsciiArray(this byte[] str)
        {
            return Encoding.ASCII.GetBytes(string.Join("", str.Select(t => t.ToString("X2"))));
        }

        /// <summary>
        /// Int转二进制
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minLength">补0长度</param>
        /// <returns></returns>
        public static string IntToBinaryArray(this int value, int minLength = 0)
        {
            //Convert.ToString(12,2); // 将12转为2进制字符串，结果 “1100”
            return Convert.ToString(value, 2).PadLeft(minLength, '0');
        }

        /// <summary>
        /// 二进制转Int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int BinaryArrayToInt(this string value)
        {
            //Convert.ToInt("1100",2); // 将2进制字符串转为整数，结果 12
            return Convert.ToInt32(value, 2);
        }
    }
}
