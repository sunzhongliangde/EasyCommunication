using System.ComponentModel;

namespace EasyCommunication
{
    /// <summary>
    /// Modbus功能码
    /// </summary>
    [Description("Modbus功能码")]
    public enum ModbusFunctionCodes
    {
        /// <summary>
        /// 读取线圈状态
        /// </summary>
        ReadCoils = 1,

        /// <summary>
        /// 读取输入状态
        /// </summary>
        ReadInputs = 2,

        /// <summary>
        /// 读取保持寄存器状态
        /// </summary>
        ReadHoldingRegisters = 3,

        /// <summary>
        /// 读取输入寄存器状态
        /// </summary>
        ReadInputRegisters = 4,

        /// <summary>
        /// 写单个线圈
        /// </summary>
        WriteSingleCoil = 5,

        /// <summary>
        /// 写单个寄存器
        /// </summary>
        WriteSingleRegister = 6,

        /// <summary>
        /// 写多个线圈
        /// </summary>
        WriteMultipleCoils = 15,

        /// <summary>
        /// 写多个寄存器
        /// </summary>
        WriteMultipleRegisters = 16,
    }

    /// <summary>
    /// Modbus 协议
    /// </summary>
    public enum ModbusType
    {
        /// <summary>
        /// Modbus TCP
        /// </summary>
        TCP = 1,

        /// <summary>
        /// Modbus RTU
        /// </summary>
        RTU = 2,
    }
}