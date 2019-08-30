using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using TeachStar.Net.Diagnosis.Core.Session;

namespace TeachStar.Net.Diagnosis.Core
{
    public class NetExceptionHandler : ISessionExceptionHandler
    {
        #region Implementation of ISessionExceptionHandler

        public bool HandleException(ISessionHost host, Exception e)
        {
            if (e is SocketException || e is NetworkInformationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Net Ex:{e}");
                Console.ResetColor();
                return true;
            }
            return false;
        }

        #endregion
    }

    /// <summary>
    /// 等待用户手动关闭窗口
    /// </summary>
    public class WaiteForCloseTask:ISessionTask
    {
        #region Implementation of ISessionTask

        public void Excute(ISessionHost host)
        {
            Console.WriteLine($"等待手动关闭程序。。。。");
            var waitor = new AutoResetEvent(false);
            waitor.WaitOne();
        }

        #endregion
    }

    public class WellComeTask:ISessionTask
    {
        #region Implementation of ISessionTask

        public void Excute(ISessionHost host)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"程序已启动");
            Console.ResetColor();
        }

        #endregion
    }
}