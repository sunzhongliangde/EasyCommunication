using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using EasyCommunication.Client;
using EasyCommunication.Utility;
using EasyCommunication;
using NetCoreServer;
using EasyCommunication.Common.Helpers;
using EasyCommunication.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EasyCommunication
{
    public class ModbusRTURequestData
    {
        public static void CreateModbusRTUData(ModbusFunctionCodes code, byte slaveAddress, ushort startAddress, ushort value)
        {

        }

        /// <summary>
        /// 单个线圈写入
        /// </summary>
        /// <param name="address">寄存器起始地址</param>
        /// <param name="value">线圈状态</param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        public byte[] Write(string address, bool value, byte stationNumber = 1)
        {
            byte[] command = ModbusRTURequestData.GetWriteCoilCommand(address, value, stationNumber, 5);
            byte[] commandCRC16 = CRC16.GetCRC16(command);

            byte[] mergedArray = command.Concat(commandCRC16).ToArray();
            return mergedArray;
        }
        /// <summary>
        /// 多条写入
        /// </summary>
        /// <param name="address">寄存器起始地址</param>
        /// <param name="values">数据集合</param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        /// <returns></returns>
        public byte[] Write(string address, byte[] values, byte stationNumber = 1, byte functionCode = 16)
        {
            values = values.ByteFormatting(EndianFormat.ABCD);
            var command = GetWriteCommand(address, values, stationNumber, functionCode);
            var commandCRC16 = CRC16.GetCRC16(command);

            byte[] mergedArray = command.Concat(commandCRC16).ToArray();
            return mergedArray;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="address">寄存器起始地址</param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="readLength">读取长度</param>
        /// <returns></returns>
        public byte[] Read(string address, byte stationNumber = 1, byte functionCode = 3, ushort readLength = 1)
        {
            //获取命令（组装报文）
            byte[] command = GetReadCommand(address, stationNumber, functionCode, readLength);
            var commandCRC16 = CRC16.GetCRC16(command);

            byte[] mergedArray = command.Concat(commandCRC16).ToArray();
            return mergedArray;
        }


        public byte[] GenerateModbusRTU(byte stationNumber, ModbusFunctionCodes functionCode, string address, string data)
        {
            List<byte> sendData = new List<byte>();
            // 站号
            sendData.Add(stationNumber);
            // 功能码
            sendData.Add(Convert.ToByte(functionCode));

            // 地址
            UInt16 addressInt16 = Convert.ToUInt16(string.Format("0x{0}", address), 16);
            byte[] addressByte = BitConverter.GetBytes(addressInt16);
            sendData.AddRange(addressByte.Reverse());

            // 0F：写多个线圈 (WriteMultipleCoils)
            if (functionCode == ModbusFunctionCodes.WriteMultipleCoils)
            {
                string[] array = data.Trim().Split(' ');
                // 添加数量
                UInt16 sourceInt16 = Convert.ToUInt16(string.Format("0x{0}", array.Length), 16);
                byte[] sourceByte = BitConverter.GetBytes(sourceInt16);
                sendData.AddRange(sourceByte.Reverse());
                sendData.Add(byte.Parse("1"));
                // 添加数据
                string c = Convert.ToInt32(string.Join("", array), 2).ToString("X2");
                UInt16 cInt16 = Convert.ToUInt16(string.Format("0x{0}", c), 16);
                sendData.Add(Convert.ToByte(cInt16));
            }
            else if (functionCode == ModbusFunctionCodes.WriteMultipleRegisters)// 10: 写多个寄存器(WriteMultipleRegisters)
            {
                string[] array = data.Trim().Split(' ');
                // 添加数量
                UInt16 countInt16 = Convert.ToUInt16(string.Format("0x{0}", array.Length), 16);
                byte[] countByte = BitConverter.GetBytes(countInt16);
                sendData.AddRange(countByte.Reverse());

                byte sourceInt16 = Convert.ToByte(array.Length * 2);
                sendData.Add(sourceInt16);
                // 添加数据
                foreach (string item in array)
                {
                    UInt16 registersInt16 = Convert.ToUInt16(string.Format("0x{0}", item), 16);
                    byte[] registersByteArray = BitConverter.GetBytes(registersInt16);
                    sendData.AddRange(registersByteArray.Reverse());
                }
            }
            else
            {
                if (functionCode == ModbusFunctionCodes.WriteSingleCoil)
                {
                    data = data == "0" ? "0" : "ff00";
                }
                // 数据
                UInt16 sourceInt16 = Convert.ToUInt16(string.Format("0x{0}", data), 16);
                byte[] sourceByte = BitConverter.GetBytes(sourceInt16);
                sendData.AddRange(sourceByte.Reverse());
            }

            var crcByte = ModbusUtility.CalculateCrc(sendData.ToArray());
            sendData.AddRange(crcByte);
            return sendData.ToArray();
        }

        #region 私有方法
        /// <summary>
        /// 获取写入命令
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="values">数据集合</param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        /// <returns></returns>
        private static byte[] GetWriteCommand(string address, byte[] values, byte stationNumber, byte functionCode)
        {
            var writeAddress = ushort.Parse(address.Trim());

            byte[] buffer = new byte[7 + values.Length];
            buffer[0] = stationNumber; //站号
            buffer[1] = functionCode;  //功能码
            buffer[2] = BitConverter.GetBytes(writeAddress)[1];
            buffer[3] = BitConverter.GetBytes(writeAddress)[0];//寄存器地址
            buffer[4] = (byte)(values.Length / 2 / 256);
            buffer[5] = (byte)(values.Length / 2 % 256);//写寄存器数量(除2是两个字节一个寄存器，寄存器16位。除以256是byte最大存储255。)              
            buffer[6] = (byte)(values.Length);          //写字节的个数
            values.CopyTo(buffer, 7);                   //把目标值附加到数组后面
            return buffer;
        }

        /// <summary>
        /// 获取线圈写入命令
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="value">线圈通断</param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        /// <returns></returns>
        private static byte[] GetWriteCoilCommand(string address, bool value, byte stationNumber, byte functionCode)
        {
            var writeAddress = ushort.Parse(address.Trim());

            byte[] buffer = new byte[6];
            buffer[0] = stationNumber;//站号
            buffer[1] = functionCode; //功能码
            buffer[2] = BitConverter.GetBytes(writeAddress)[1];
            buffer[3] = BitConverter.GetBytes(writeAddress)[0];//寄存器地址
            buffer[4] = (byte)(value ? 0xFF : 0x00);     //此处只可以是FF表示闭合00表示断开，其他数值非法
            buffer[5] = 0x00;
            return buffer;
        }
        /// <summary>
        /// 获取读取命令
        /// </summary>
        /// <param name="address">寄存器起始地址</param>
        /// <param name="stationNumber">站号</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="length">读取长度</param>
        /// <returns></returns>
        private byte[] GetReadCommand(string address, byte stationNumber, byte functionCode, ushort length)
        {
            var readAddress = ushort.Parse(address.Trim());

            byte[] buffer = new byte[6];
            buffer[0] = stationNumber;  //站号
            buffer[1] = functionCode;   //功能码
            buffer[2] = BitConverter.GetBytes(readAddress)[1];
            buffer[3] = BitConverter.GetBytes(readAddress)[0];//寄存器地址
            buffer[4] = BitConverter.GetBytes(length)[1];
            buffer[5] = BitConverter.GetBytes(length)[0];//表示request 寄存器的长度(寄存器个数)
            return buffer;
        }
        #endregion
    }
}
