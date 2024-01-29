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
                    bool connected = client.ConnectAsync();
                    client.OnReceivedEvent += Client_OnReceivedEvent;
                    if (connected)
                    {
                        txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 已连接\r\n");
                        btn_connect.Text = "断开";
                    }
                }
                else
                {
                    client?.DisconnectAsync();
                    btn_connect.Text = "连接";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
