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

        public delegate void OnReceivedDelegate(byte[] buffer, long offset, long size);
        /// <summary>
        /// 客户端收到服务端发过来的消息
        /// </summary>
        public event OnReceivedDelegate? OnReceivedEvent;

        public delegate void OnConnectedDelegate();
        /// <summary>
        /// 已连接
        /// </summary>
        public event OnConnectedDelegate? OnConnectedEvent;

        public delegate void OnDisconnectedDelegate();
        /// <summary>
        /// 已断开连接
        /// </summary>
        public event OnDisconnectedDelegate? OnDisconnectedEvent;


        /// <summary>
        /// 设备收到消息
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            base.OnReceived(buffer, offset, size);
            OnReceivedEvent?.Invoke(buffer, offset, size);
        }

        /// <summary>
        /// 设备已连接到Server
        /// </summary>
        protected override void OnConnected()
        {
            base.OnConnected();
            OnConnectedEvent?.Invoke();
        }

        protected override void OnDisconnected()
        {
            base.OnDisconnected();
            OnDisconnectedEvent?.Invoke();
        }
    }
}
