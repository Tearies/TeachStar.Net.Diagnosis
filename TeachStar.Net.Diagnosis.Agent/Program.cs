using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachStar.Net.Diagnosis.Core.Session;

namespace TeachStar.Net.Diagnosis.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            var session = Session.Factory.StartNew<Session>();
            session.FilterException<NetExceptionHandler>();
            session.Use<LogCurrentIpTask>();
            session.Run();
        }
    }
}
