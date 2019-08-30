using System.Threading.Tasks;

namespace TeachStar.Net.Diagnosis.Core.Session
{
    public interface ISessionTask
    {
        void Excute(ISessionHost host);
    }
}