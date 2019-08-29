namespace TeachStar.Net.Diagnosis.Core.Session
{
    public static class ISessionHostExtension
    {
        public static ISessionHost Use<T>(this ISessionHost host) where T : ISessionTask
        {
            host.Use<T>();
            return host;
        }
    }
}