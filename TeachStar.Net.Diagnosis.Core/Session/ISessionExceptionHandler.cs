using System;

namespace TeachStar.Net.Diagnosis.Core.Session
{
    public interface ISessionExceptionHandler
    {
        bool HandleException(ISessionHost host, Exception e);
    }
}