using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EasyCommunication.Utility;

namespace EasyCommunication
{
    public class ModbusRTURequestData
    {
        public static void CreateModbusRTUData(ModbusFunctionCodes code, byte slaveAddress, ushort startAddress, ushort value)
        {

        }

        public byte[] GenerateModbusRTU(byte slaveAddress, ModbusFunctionCodes functionCode, string address, string data)
        {
            List<byte> sendData = new List<byte>();
            // 站号
            sendData.Add(slaveAddress);
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

                //foreach (string item in array)
                //{
                //    var ditem = item == "0" ? "0" : "ff00";
                //    UInt16 coilsInt16 = Convert.ToUInt16(string.Format("0x{0}", ditem), 16);
                //    byte[] coilsByteArray = BitConverter.GetBytes(coilsInt16);
                //    sendData.AddRange(coilsByteArray.Reverse());
                //}
                sendData.Add(Convert.ToByte(string.Join("", array).PadLeft(16, '0')));
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
    }
}
