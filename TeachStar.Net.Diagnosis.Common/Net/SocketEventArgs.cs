using System;
using System.Net;

namespace TeachStar.Net.Diagnosis.Common.Net
{
    public class SocketEventArgs : EventArgs, IDisposable
    {
        public SocketEventArgs(IPAddress remoteIpAddress,int port, string messageBuffer)
        {
            RemoteIpAddress = remoteIpAddress;
            MessageBuffer = messageBuffer;
            Port = port;
        }

        public int Port { get; private set; }

        /// <summary>
        /// 远程地址
        /// </summary>
        public IPAddress RemoteIpAddress { get; private set; }

        /// <summary>
        /// 消息Buffer
        /// </summary>
        public string MessageBuffer { get; private set; }

        public void Dispose()
        {

        }
    }
}