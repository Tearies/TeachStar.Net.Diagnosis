using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;

namespace TeachStar.Net.Diagnosis.Core.Session
{
    public class Session : ISessionHost
    {
        public static readonly SessionFactory Factory = new Lazy<SessionFactory>(() => new SessionFactory()).Value;
        protected string SessionId { get; private set; }
        private List<ISessionTask> internalTasks;
        private List<ISessionExceptionHandler> internalExceptionHandler;
        public Session(string sessionId)
        {
            internalTasks = new List<ISessionTask>();
            internalExceptionHandler = new List<ISessionExceptionHandler>();
            SessionId = sessionId;
        }

        #region Implementation of ISessionHost

        public ISessionHost Use<T>() where T : ISessionTask
        {
            if (!internalTasks.Any(o => o is T))
                internalTasks.Add(Activator.CreateInstance<T>());
            return this;
        }

        public ISessionHost FilterException<T>() where T : ISessionExceptionHandler
        {
            if (!internalExceptionHandler.Any(o => o is T))
                internalExceptionHandler.Add(Activator.CreateInstance<T>());
            return this;
        }

        public void Run()
        {
            try
            {
                internalTasks.ForEach(p =>
                {
                    p.Excute(this);
                });
            }
            catch (Exception e)
            {
                if (internalExceptionHandler.Any())
                {
                    internalExceptionHandler.ForEach(p =>
                    {
                        try
                        {
                            if (p.HandleException(this, e))
                            {
                                return;
                            }
                        }
                        catch (Exception x)
                        {
                            Console.WriteLine($"UnhandedException:{x}");
                        }
                    });
                }
            }
        }

        #endregion
    }
}