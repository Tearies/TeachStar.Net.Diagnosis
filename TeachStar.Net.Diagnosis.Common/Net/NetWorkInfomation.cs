using System.Collections.Generic;
using System.Linq;
using System.Net;
using TeachStar.Net.Diagnosis.Core.Helper;

namespace TeachStar.Net.Diagnosis.Common.Net
{
    public class NetWorkInfomation
    {

        public static readonly string LocalHost = "127.0.0.1";
        public static DiagnosisResult DiagnosisIpAddress(string ipAddress)
        {
            var localInfos = LocalNetDevice.RefreshNetInfos();
            List<NetAddressInfo> findAddress = null;
            if (localInfos.Count(o => o.GateWay.IsValid()) > 1)
            {
                try
                {
                    if (IPAddress.TryParse(ipAddress, out IPAddress target))
                    {
                        using (var tracert = new NetDiagnosis(target, maxHops: 5))
                        {
                            tracert.Tracert();
                            tracert.Waite();
                            var defaultNode = tracert.RouteNode;
                            if (defaultNode.Address.ToString() != "0.0.0.0" && defaultNode.Address.ToString() != LocalHost)
                            {
                                findAddress = localInfos.Where(o =>
                                    o.AddressFamily == defaultNode.Address.AddressFamily &&
                                    (defaultNode.Address.ToString() == o.GateWay && tracert.InSameVLan(o.Ip))).ToList();
                                if (findAddress.IsInvalid())
                                {
                                    findAddress = localInfos.Where(o =>
                                            o.AddressFamily == defaultNode.Address.AddressFamily &&
                                            tracert.InSameVLan(o.Ip))
                                        .ToList();
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
            }

            if (findAddress.IsInvalid())
            {
                var ips = localInfos.Where(o => o.GateWay != null).ToList();
                if (ips.IsInvalid())
                {
                    ips = new List<NetAddressInfo>() { localInfos.FirstOrDefault() };
                }
                findAddress = ips;
            }

            return new DiagnosisResult(findAddress.ToList(), findAddress.FirstOrDefault());
        }

        public static List<NetAddressInfo> GetLocalNetInfos()
        {
            return LocalNetDevice.RefreshNetInfos();
        }
    }
}