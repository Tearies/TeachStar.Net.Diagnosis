namespace TeachStar.Net.Diagnosis.Core.Session
{
    
    public interface ISessionHost
    {
        ISessionHost Use<T>() where T : ISessionTask;
        ISessionHost FilterException<T>() where T : ISessionExceptionHandler;
        void Run();
    }
}