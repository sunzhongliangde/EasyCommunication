using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyCommunication.TCP;

namespace EasyCommunicationDemo
{
    public partial class ModbusTcpClientForm : Form
    {
        ModbusTcpClient? client;
        public ModbusTcpClientForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_connect.Text == "连接")
                {
                    client = new ModbusTcpClient(txt_ipAddress.Text, int.Parse(txt_port.Text));
                    client.ConnectAsync();
                    client.OnReceivedEvent += Client_OnReceivedEvent;
                    client.OnConnectedEvent += Client_OnConnectedEvent;
                    client.OnDisconnectedEvent += Client_OnDisconnectedEvent;
                }
                else
                {
                    client?.DisconnectAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 设备已断开连接
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Client_OnDisconnectedEvent()
        {
            btn_connect.Text = "连接";
        }

        /// <summary>
        /// 设备已连接到Server
        /// </summary>
        private void Client_OnConnectedEvent()
        {
            if (client != null && client.IsConnected)
            {
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 已连接\r\n");
                btn_connect.Text = "断开";
            }
        }

        private void Client_OnReceivedEvent(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            this.Invoke(new Action(() =>
            {
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 收到消息：{message}\r\n");
            }));


        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            long? size = client?.Send(txt_message.Text);
            if (size != null && size != 0)
            {
                txt_message.Text = "";
            }
        }

        private void ModbusTcpClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client?.Disconnect();
            client?.Dispose();
        }
    }
}
