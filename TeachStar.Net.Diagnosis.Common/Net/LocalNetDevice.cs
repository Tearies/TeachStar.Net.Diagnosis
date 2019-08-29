using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TeachStar.Net.Diagnosis.Common.Net
{
    internal class LocalNetDevice
    {
        internal static List<NetAddressInfo> RefreshNetInfos()
        {
            var localNetInfos = new List<NetAddressInfo>();
            var localInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var netDevice in localInterfaces)
            {
                var ippropertys = netDevice.GetIPProperties();
                var gatwayInfos = ippropertys.GatewayAddresses;
                var ipaddress = ippropertys.UnicastAddresses;
                var macaddress = string.Join("-", netDevice.GetPhysicalAddress().GetAddressBytes().Select(o => o.ToString("X2")));
                var deviceName = netDevice.Description;
                var conName = netDevice.Name;
                int index = 0;
                foreach (var ip in ipaddress)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        var curNetAddressInfo = new NetAddressInfo();
                        curNetAddressInfo.Ip = ip.Address.ToString();
                        curNetAddressInfo.MacAddress = macaddress;
                        curNetAddressInfo.DeviceName = deviceName;
                        curNetAddressInfo.ConnectName = conName;
                        curNetAddressInfo.AddressFamily = ip.Address.AddressFamily;
                        if (gatwayInfos.Any())
                        {
                            /*
                             * 只取同一张网卡的默认GatWay
                             */
                            curNetAddressInfo.GateWay = gatwayInfos[0].Address.ToString();
                        }
                        localNetInfos.Add(curNetAddressInfo);
                    }
                  
                }

            }

            return localNetInfos;
        }
    }
}