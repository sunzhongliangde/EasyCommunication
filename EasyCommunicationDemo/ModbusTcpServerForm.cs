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
                string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
                txt_log.AppendText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} �յ���Ϣ��{message}\r\n");
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
            if (txt_sendMsg.Text.Length == 0)
            {
                MessageBox.Show("������Ϣ����Ϊ��");
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
