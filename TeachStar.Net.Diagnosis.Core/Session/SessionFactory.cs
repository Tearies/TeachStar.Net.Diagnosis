using System;

namespace TeachStar.Net.Diagnosis.Core.Session
{
    public class SessionFactory
    {
        internal SessionFactory()
        {

        }
        public ISessionHost StartNew<T>() where T : ISessionHost
        {
            var host= (T)Activator.CreateInstance(typeof(T), Guid.NewGuid().ToString("N"));
            return host;
        }
    }
}