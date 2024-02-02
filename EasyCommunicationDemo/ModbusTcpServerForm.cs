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
            // �����ַ��Ϸ�
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
                    toolTip1.SetToolTip(txt, "16���Ƹ�ʽ���������ʹ�ÿո����");
                }
                else
                {
                    toolTip1.SetToolTip(txt, "16���Ƹ�ʽ");
                }
            }
        }


        /// <summary>
        /// ����/�Ͽ� TCP�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_tcpListener_Click(object sender, EventArgs e)
        {
            if (btn_tcpListener.Text == "����")
            {
                if (!int.TryParse(txt_tcpPort.Text, out int port))
                {
                    MessageBox.Show("�˿ںŲ��Ϸ�");
                    return;
                }
                if (server != null && server.IsStarted)
                {
                    btn_tcpListener.Text = "�Ͽ�";
                    MessageBox.Show("Tcp�����ѿ���");
                    return;
                }
                server = new ModbusTcpServer(IPAddress.Any, port);
                server.OnConnectedEvent += Server_OnConnectedEvent;
                server.OnDisconnectedEvent += Server_OnDisconnectedEvent;
                server.OnReceivedEvent += Server_OnReceivedEvent;

                bool isStart = server.Start();
                if (isStart)
                {
                    btn_tcpListener.Text = "�Ͽ�";
                    txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} TCP�����ѿ���\r\n");
                }
                else
                {
                    MessageBox.Show("TCP������ʧ�ܣ�����˿�");
                }
            }
            else
            {
                if (server != null && server.Stop())
                {
                    btn_tcpListener.Text = "����";
                    txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} TCP�����ѶϿ�\r\n");
                }
                else
                {
                    MessageBox.Show("TCP����Ͽ�ʧ�ܣ�����˿�");
                }

            }
        }

        /// <summary>
        /// �յ�����Ϣ
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
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} �յ���Ϣ��{BitConverter.ToString(buffer, 0, (int)size)}\r\n");
                var reportData = new ClientReportDataFormat(buffer, offset, size);
            }));
        }

        /// <summary>
        /// �ͻ��������ѶϿ�
        /// </summary>
        /// <param name="session"></param>
        private void Server_OnDisconnectedEvent(NetCoreServer.TcpSession session)
        {
            this.Invoke(new Action(() =>
            {
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} �ͻ���{session.Id.ToString()}�ѶϿ�\r\n");
            }));
        }

        /// <summary>
        /// �ͻ��������ѽ���
        /// </summary>
        /// <param name="session"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Server_OnConnectedEvent(NetCoreServer.TcpSession session)
        {
            this.Invoke(new Action(() =>
            {
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} �ͻ���{session.Id.ToString()}������\r\n");
            }));
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            if (txt_sendAddress.Text.Length == 0)
            {
                MessageBox.Show("������Ϣ����Ϊ��");
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
        /// ��Modbus������
        /// </summary>
        private void BindFunctionCodeDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value", int.MaxValue.GetType());

            var row1 = dt.NewRow();
            row1["name"] = "01H������Ȧ";
            row1["value"] = (int)ModbusFunctionCodes.ReadCoils;
            dt.Rows.Add(row1);

            var row2 = dt.NewRow();
            row2["name"] = "02H����ȡ����״̬";
            row2["value"] = (int)ModbusFunctionCodes.ReadInputs;
            dt.Rows.Add(row2);

            var row3 = dt.NewRow();
            row3["name"] = "03H����ȡ���ּĴ���";
            row3["value"] = (int)ModbusFunctionCodes.ReadHoldingRegisters;
            dt.Rows.Add(row3);

            var row4 = dt.NewRow();
            row4["name"] = "04H����ȡ����Ĵ���";
            row4["value"] = (int)ModbusFunctionCodes.ReadInputRegisters;
            dt.Rows.Add(row4);

            var row5 = dt.NewRow();
            row5["name"] = "05H��д������Ȧ";
            row5["value"] = (int)ModbusFunctionCodes.WriteSingleCoil;
            dt.Rows.Add(row5);

            var row6 = dt.NewRow();
            row6["name"] = "06H��д�����Ĵ���";
            row6["value"] = (int)ModbusFunctionCodes.WriteSingleRegister;
            dt.Rows.Add(row6);

            var row7 = dt.NewRow();
            row7["name"] = "0FH��д�����Ȧ";
            row7["value"] = (int)ModbusFunctionCodes.WriteMultipleCoils;
            dt.Rows.Add(row7);

            var row8 = dt.NewRow();
            row8["name"] = "10H��д����Ĵ���";
            row8["value"] = (int)ModbusFunctionCodes.WriteMultipleRegisters;
            dt.Rows.Add(row8);

            cbx_functionCode.DisplayMember = "name";
            cbx_functionCode.ValueMember = "value";
            cbx_functionCode.DataSource = dt;
        }

        /// <summary>
        /// ��վ��
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
                // ����ModbusԤ��ָ������
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
                //�������뽹�������һ���ַ�
                textbox.SelectionStart = textbox.Text.Length;
            }

            // ����ModbusԤ��ָ������
            BuildModbusCommand();
        }

        /// <summary>
        /// Modbus����������������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_functionCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbx_functionCode.SelectedIndex)
            {
                case 0:
                    txt_functionText.Text = "��Ȧ������";
                    break;
                case 1:
                    txt_functionText.Text = "������������";
                    break;
                case 2:
                    txt_functionText.Text = "�Ĵ���������";
                    break;
                case 3:
                    txt_functionText.Text = "�Ĵ���������";
                    break;
                case 4:
                    txt_functionText.Text = "��Ȧ״ֵ̬��";
                    break;
                case 5:
                    txt_functionText.Text = "�Ĵ������ݣ�";
                    break;
                case 6:
                    txt_functionText.Text = "��Ȧ״ֵ̬��";
                    break;
                default:
                    txt_functionText.Text = "�Ĵ������ݣ�";
                    break;
            }
            if (cbx_functionCode.SelectedIndex >= 6)
            {
                // ȡ����������
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
        /// ����ָ��Ԥ������
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
