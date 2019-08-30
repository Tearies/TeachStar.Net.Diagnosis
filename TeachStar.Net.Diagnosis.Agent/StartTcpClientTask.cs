using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeachStar.Net.Diagnosis.Common.Net;
using TeachStar.Net.Diagnosis.Core.Session;

namespace TeachStar.Net.Diagnosis.Agent
{
    internal class StartTcpClientTask : ISessionTask
    {
        private string inde;
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public StartTcpClientTask()
        {
            inde = Guid.NewGuid().ToString("N");
        }

        #region Implementation of ISessionTask

        public void Excute(ISessionHost host)
        {
        A:
            Console.WriteLine("请输入要连接的服务器地址:");
            var address = Console.ReadLine();
            if (!IPAddress.TryParse(address, out IPAddress server))
                goto A;

            B:
            Console.WriteLine("请输入要连接的服务器端口:");
            var port = Console.ReadLine();
            if (!int.TryParse(port, out int tcpPort))
            {
                goto B;
            }

            var tcpClient = new TcpClient();
            var waitor = new AutoResetEvent(false);
            tcpClient.ConnectAsync(server, tcpPort).ContinueWith(p =>
            {
                Console.WriteLine($"{server}:{port} 连接:{tcpClient.Connected}");
                if (tcpClient.Connected)
                {
                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                tcpClient.Client.Send(Encoding.UTF8.GetBytes($"{inde}-->{DateTime.Now:O}"));
                            }
                            catch(Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(e);
                                Console.ResetColor();
                                break;
                            }
                        
                            Thread.Sleep(200);
                        }
                    });
                  
                }
            });
            waitor.WaitOne();
        }

        #endregion
    }
}