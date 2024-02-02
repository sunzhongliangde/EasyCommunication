using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using EasyCommunication;
using EasyCommunication.DataFormat;
using EasyCommunication.TCP;

namespace EasyCommunicationDemo
{
    public partial class ModbusTcpServerForm : Form
    {
        ModbusTcpServer? server = null;
        public ModbusTcpServerForm()
        {
            InitializeComponent();
            BindFunctionCodeDataSource();
            BindSlaveAddress();
            cbx_Mode.SelectedIndex = 0;
            // 限制字符合法
            txt_sendAddress.TextChanged += TextChangedEvent;
            txt_sendData.TextChanged += TextChangedEvent;
            txt_sendAddress.MouseMove += Txt_sendAddress_GotFocus;
            txt_sendData.MouseMove += Txt_sendAddress_GotFocus;
        }

        private void Txt_sendAddress_GotFocus(object? sender, EventArgs e)
        {
            TextBox? txt = sender as TextBox;
            if (txt != null)
            {
                if (cbx_functionCode.SelectedIndex >= 6 && txt.TabIndex == 12)
                {
                    toolTip1.SetToolTip(txt, "16进制格式，多个数据使用空格隔开");
                }
                else
                {
                    toolTip1.SetToolTip(txt, "16进制格式");
                }
            }
        }


        /// <summary>
        /// 开启/断开 TCP服务端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_tcpListener_Click(object sender, EventArgs e)
        {
            if (btn_tcpListener.Text == "监听")
            {
                if (!int.TryParse(txt_tcpPort.Text, out int port))
                {
                    MessageBox.Show("端口号不合法");
                    return;
                }
                if (server != null && server.IsStarted)
                {
                    btn_tcpListener.Text = "断开";
                    MessageBox.Show("Tcp服务已开启");
                    return;
                }
                server = new ModbusTcpServer(IPAddress.Any, port);
                server.OnConnectedEvent += Server_OnConnectedEvent;
                server.OnDisconnectedEvent += Server_OnDisconnectedEvent;
                server.OnReceivedEvent += Server_OnReceivedEvent;

                bool isStart = server.Start();
                if (isStart)
                {
                    btn_tcpListener.Text = "断开";
                    txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} TCP服务已开启\r\n");
                }
                else
                {
                    MessageBox.Show("TCP服务开启失败，请检查端口");
                }
            }
            else
            {
                if (server != null && server.Stop())
                {
                    btn_tcpListener.Text = "监听";
                    txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} TCP服务已断开\r\n");
                }
                else
                {
                    MessageBox.Show("TCP服务断开失败，请检查端口");
                }

            }
        }

        /// <summary>
        /// 收到新消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Server_OnReceivedEvent(NetCoreServer.TcpSession session, byte[] buffer, long offset, long size)
        {
            this.Invoke(new Action(() =>
            {
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 收到消息：{BitConverter.ToString(buffer, 0, (int)size)}\r\n");
                var reportData = new ClientReportDataFormat(buffer, offset, size);
            }));
        }

        /// <summary>
        /// 客户端连接已断开
        /// </summary>
        /// <param name="session"></param>
        private void Server_OnDisconnectedEvent(NetCoreServer.TcpSession session)
        {
            this.Invoke(new Action(() =>
            {
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 客户端{session.Id.ToString()}已断开\r\n");
            }));
        }

        /// <summary>
        /// 客户端连接已建立
        /// </summary>
        /// <param name="session"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Server_OnConnectedEvent(NetCoreServer.TcpSession session)
        {
            this.Invoke(new Action(() =>
            {
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 客户端{session.Id.ToString()}已连接\r\n");
            }));
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            if (txt_sendAddress.Text.Length == 0)
            {
                MessageBox.Show("发送消息不能为空");
                return;
            }

            var codde = Convert.ToInt32(cbx_functionCode.SelectedValue);
            var request = new ModbusRTURequestData();
            byte[] sendDatas = request.GenerateModbusRTU(Convert.ToByte(cbx_slaveAddress.SelectedValue), (ModbusFunctionCodes)codde, txt_sendAddress.Text, txt_sendData.Text);

            bool? isSend = server?.Multicast(sendDatas.ToArray());
        }

        private void ModbusTcpServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            server?.Stop();
            server?.Dispose();
        }

        /// <summary>
        /// 绑定Modbus功能码
        /// </summary>
        private void BindFunctionCodeDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value", int.MaxValue.GetType());

            var row1 = dt.NewRow();
            row1["name"] = "01H：读线圈";
            row1["value"] = (int)ModbusFunctionCodes.ReadCoils;
            dt.Rows.Add(row1);

            var row2 = dt.NewRow();
            row2["name"] = "02H：读取输入状态";
            row2["value"] = (int)ModbusFunctionCodes.ReadInputs;
            dt.Rows.Add(row2);

            var row3 = dt.NewRow();
            row3["name"] = "03H：读取保持寄存器";
            row3["value"] = (int)ModbusFunctionCodes.ReadHoldingRegisters;
            dt.Rows.Add(row3);

            var row4 = dt.NewRow();
            row4["name"] = "04H：读取输入寄存器";
            row4["value"] = (int)ModbusFunctionCodes.ReadInputRegisters;
            dt.Rows.Add(row4);

            var row5 = dt.NewRow();
            row5["name"] = "05H：写单个线圈";
            row5["value"] = (int)ModbusFunctionCodes.WriteSingleCoil;
            dt.Rows.Add(row5);

            var row6 = dt.NewRow();
            row6["name"] = "06H：写单个寄存器";
            row6["value"] = (int)ModbusFunctionCodes.WriteSingleRegister;
            dt.Rows.Add(row6);

            var row7 = dt.NewRow();
            row7["name"] = "0FH：写多个线圈";
            row7["value"] = (int)ModbusFunctionCodes.WriteMultipleCoils;
            dt.Rows.Add(row7);

            var row8 = dt.NewRow();
            row8["name"] = "10H：写多个寄存器";
            row8["value"] = (int)ModbusFunctionCodes.WriteMultipleRegisters;
            dt.Rows.Add(row8);

            cbx_functionCode.DisplayMember = "name";
            cbx_functionCode.ValueMember = "value";
            cbx_functionCode.DataSource = dt;
        }

        /// <summary>
        /// 绑定站号
        /// </summary>
        private void BindSlaveAddress()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value", int.MaxValue.GetType());
            for (int i = 1; i <= 255; i++)
            {
                var row = dt.NewRow();

                row["name"] = string.Format("0x{0}", i.ToString("X").PadLeft(2, '0'));
                row["value"] = i;
                dt.Rows.Add(row);
            }
            cbx_slaveAddress.DisplayMember = "name";
            cbx_slaveAddress.ValueMember = "value";
            cbx_slaveAddress.DataSource = dt;
        }

        private void TextChangedEvent(object? sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null)
            {
                return;
            }
            if (cbx_functionCode.SelectedIndex >= 6)
            {
                // 生成Modbus预览指令数据
                BuildModbusCommand();
                return;
            }
            var reg = new Regex("^[0-9A-Fa-f]+$");
            var str = textbox.Text.Trim();
            var sb = new StringBuilder();
            if (!reg.IsMatch(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (reg.IsMatch(str[i].ToString()))
                    {
                        sb.Append(str[i].ToString());
                    }
                }
                textbox.Text = sb.ToString();
                //定义输入焦点在最后一个字符
                textbox.SelectionStart = textbox.Text.Length;
            }

            // 生成Modbus预览指令数据
            BuildModbusCommand();
        }

        /// <summary>
        /// Modbus功能码下拉框更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_functionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbx_functionCode.SelectedIndex)
            {
                case 0:
                    txt_functionText.Text = "线圈数量：";
                    break;
                case 1:
                    txt_functionText.Text = "输入区数量：";
                    break;
                case 2:
                    txt_functionText.Text = "寄存器数量：";
                    break;
                case 3:
                    txt_functionText.Text = "寄存器数量：";
                    break;
                case 4:
                    txt_functionText.Text = "线圈状态值：";
                    break;
                case 5:
                    txt_functionText.Text = "寄存器数据：";
                    break;
                case 6:
                    txt_functionText.Text = "线圈状态值：";
                    break;
                default:
                    txt_functionText.Text = "寄存器数据：";
                    break;
            }
            if (cbx_functionCode.SelectedIndex >= 6)
            {
                // 取消长度限制
                txt_sendData.MaxLength = 128;
            }
            else
            {
                txt_sendData.MaxLength = 4;
            }
            txt_sendData.Text = "";
            BuildModbusCommand();
        }

        /// <summary>
        /// 生成指令预览功能
        /// </summary>
        private void BuildModbusCommand()
        {
            if (txt_sendAddress.Text.Trim().Length == 0 || txt_sendData.Text.Trim().Length == 0)
            {
                return;
            }

            var codde = Convert.ToInt32(cbx_functionCode.SelectedValue);

            var request = new ModbusRTURequestData();
            byte[] sendDatas = request.GenerateModbusRTU(Convert.ToByte(cbx_slaveAddress.SelectedValue), (ModbusFunctionCodes)codde, txt_sendAddress.Text, txt_sendData.Text);

            List<string> commandList = new List<string>();
            foreach (byte data in sendDatas)
            {
                commandList.Add(data.ToString("X").PadLeft(2, '0'));
            }
            txt_preview.Text = string.Join(',', commandList);
        }

    }
}
