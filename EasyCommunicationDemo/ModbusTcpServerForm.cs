using System;
using System.Data;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Text;
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

            byte[] sendData = new byte[4];
            // վ��
            sendData[0] = (Convert.ToByte(cbx_slaveAddress.SelectedValue));
            // ������
            sendData[1] = (Convert.ToByte(cbx_functionCode.SelectedValue));
            // ��ַ
            sendData[2] = (Convert.ToByte(txt_sendAddress.Text));
            // ����
            sendData[3] = (Convert.ToByte(txt_sendData.Text));


            bool? isSend = server?.Multicast(sendData);
            if (isSend != null && isSend == true)
            {
                txt_sendAddress.Text = "";
            }
        }

        private void ModbusTcpServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            server?.Stop();
            server?.Dispose();
        }

        private void BindFunctionCodeDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value");

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

        private void BindSlaveAddress()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value", byte.MaxValue.GetType());
            for (int i = 1; i <= 255; i++)
            {
                var row = dt.NewRow();

                row["name"] = string.Format("0x{0}", i.ToString("X").PadLeft(2, '0'));
                row["value"] = Convert.ToByte(i);
                dt.Rows.Add(row);
            }
            cbx_slaveAddress.DisplayMember = "name";
            cbx_slaveAddress.ValueMember = "value";
            cbx_slaveAddress.DataSource = dt;
        }
    }
}
