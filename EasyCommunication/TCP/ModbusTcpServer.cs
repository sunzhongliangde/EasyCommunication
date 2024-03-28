using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;
using static System.Collections.Specialized.BitVector32;

namespace EasyCommunication.TCP
{
    /// <summary>
    /// Tcp Server
    /// </summary>
    public class ModbusTcpServer : TcpServer
    {
        // 客户端断开连接时
        public delegate void OnDisconnectedDelegate(TcpSession session);
        /// <summary>
        /// 客户端连接已建立
        /// </summary>
        public event OnDisconnectedDelegate? OnDisconnectedEvent;

        // 有新的客户端连接时
        public delegate void OnConnectedDelegate(TcpSession session);
        /// <summary>
        /// 客户端连接已断开
        /// </summary>
        public event OnConnectedDelegate? OnConnectedEvent;

        // 收到新的消息
        public delegate void OnReceivedDelegate(TcpSession session, byte[] buffer, long offset, long size);
        /// <summary>
        /// 收到新消息
        /// </summary>
        public event OnReceivedDelegate? OnReceivedEvent;


        protected override TcpSession CreateSession()
        {
            ModbusTcpSession session = new ModbusTcpSession(this);
            session.OnReceivedEvent += Session_OnReceivedEvent;
            return session;
        }

        /// <summary>
        /// 收到消息时
        /// </summary>
        /// <param name="session">客户端连接</param>
        /// <param name="buffer">收到的 buffer</param>
        /// <param name="offset">收到的 buffer offset</param>
        /// <param name="size">收到的 buffer size</param>
        private void Session_OnReceivedEvent(TcpSession session, byte[] buffer, long offset, long size)
        {
            OnReceivedEvent?.Invoke(session, buffer, offset, size);
        }

        public ModbusTcpServer(IPAddress address, int port) : base(address, port) { }

        /// <summary>
        /// 有新的客户端连接建立时
        /// </summary>
        /// <param name="session"></param>
        protected override void OnConnected(TcpSession session)
        {
            OnConnectedEvent?.Invoke(session);
            base.OnConnected(session);
        }

        /// <summary>
        /// 客户端关闭连接时
        /// </summary>
        /// <param name="session"></param>
        protected override void OnDisconnected(TcpSession session)
        {
            OnDisconnectedEvent?.Invoke(session);
            base.OnDisconnected(session);
        }


        protected override void OnError(SocketError error)
        {
            base.OnError(error);
        }
    }

    #region ChatSession Class

    public class ModbusTcpSession : TcpSession
    {
        // 收到新的消息
        public delegate void OnReceivedDelegate(TcpSession session, byte[] buffer, long offset, long size);
        /// <summary>
        /// 收到新消息
        /// </summary>
        public event OnReceivedDelegate? OnReceivedEvent;


        public ModbusTcpSession(TcpServer server) : base(server) { }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            base.OnReceived(buffer, offset, size);
            if (OnReceivedEvent != null)
            {
                OnReceivedEvent(this, buffer, offset, size);
            }
        }
    }
    #endregion
}
