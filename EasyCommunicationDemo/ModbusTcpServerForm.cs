using System.Net;
using System.Text;
using EasyCommunication.TCP;

namespace EasyCommunicationDemo
{
    public partial class ModbusTcpServerForm : Form
    {
        ModbusTcpServer? server = null;
        public ModbusTcpServerForm()
        {
            InitializeComponent();
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
                string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} 收到消息：{message}\r\n");
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
            if (txt_sendMsg.Text.Length == 0)
            {
                MessageBox.Show("发送消息不能为空");
                return;
            }
            bool? isSend = server?.Multicast(txt_sendMsg.Text);
            if (isSend != null && isSend == true)
            {
                txt_sendMsg.Text = "";
            }
        }
    }
}
