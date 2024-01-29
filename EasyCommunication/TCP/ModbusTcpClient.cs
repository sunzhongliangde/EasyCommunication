using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;

namespace EasyCommunication.TCP
{
    public class ModbusTcpClient : TcpClient
    {
        public ModbusTcpClient(string address, int port) : base(address, port) { }

        // 收到消息
        public delegate void OnReceivedDelegate(byte[] buffer, long offset, long size);
        /// <summary>
        /// 客户端收到服务端发过来的消息
        /// </summary>
        public event OnReceivedDelegate? OnReceivedEvent;



        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            base.OnReceived(buffer, offset, size);
            OnReceivedEvent?.Invoke(buffer, offset, size);
        }
    }
}
