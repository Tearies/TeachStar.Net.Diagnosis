using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using TeachStar.Net.Diagnosis.Core.Session;

namespace TeachStar.Net.Diagnosis.Agent
{
    internal class NetExceptionHandler : ISessionExceptionHandler
    {
        #region Implementation of ISessionExceptionHandler

        public bool HandleException(ISessionHost host, Exception e)
        {
            if (e is SocketException || e is NetworkInformationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Net Ex:{e}");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            return false;
        }

        #endregion
    }
}