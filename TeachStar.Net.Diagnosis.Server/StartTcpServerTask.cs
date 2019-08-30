using System;
using System.Net;
using TeachStar.Net.Diagnosis.Common.Net;
using TeachStar.Net.Diagnosis.Core.Session;

namespace TeachStar.Net.Diagnosis.Server
{
    internal class StartTcpServerTask : ISessionTask
    {
        #region Implementation of ISessionTask

        public void Excute(ISessionHost host)
        {
            var port = SocketPort.GetNextTCPPort();
            var tcpServer = new SocketListener();
            tcpServer.ErrorEvent += TcpServer_ErrorEvent;
            tcpServer.MessageReceivedEvent += TcpServer_MessageReceivedEvent;
            tcpServer.StartListener(IPAddress.Parse("127.0.0.1"), port);
        }

        private void TcpServer_MessageReceivedEvent(object sender, SocketEventArgs e)
        {
            Console.WriteLine($"{e.RemoteIpAddress}({e.Port}):{e.MessageBuffer}");
            e.Dispose();
        }

        private void TcpServer_ErrorEvent(object sender, System.IO.ErrorEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.GetException());
            Console.ResetColor();
        }

        #endregion
    }
}