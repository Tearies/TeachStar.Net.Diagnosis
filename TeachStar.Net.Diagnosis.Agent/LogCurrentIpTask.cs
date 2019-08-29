using System;
using TeachStar.Net.Diagnosis.Core.Session;

namespace TeachStar.Net.Diagnosis.Agent
{
    internal class LogCurrentIpTask:ISessionTask
    {
        #region Implementation of ISessionTask

        public void Excute(ISessionHost host)
        {
            Console.WriteLine($"host:{Net.Diagnosis.Common.Net.NetWorkInfomation.DiagnosisIpAddress("127.0.0.1").Recommend.DeviceName}");
        }

        #endregion
    }
}