using System.Threading.Tasks;
using TeachStar.Net.Diagnosis.Core;
using TeachStar.Net.Diagnosis.Core.Session;

namespace TeachStar.Net.Diagnosis.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var session = Session.Factory.StartNew<Session>();
            session.FilterException<NetExceptionHandler>();
            session.Use<WellComeTask>();
            session.Use<StartTcpServerTask>();
            session.Use<WaiteForCloseTask>();
            session.Run();
        }
    }
}
