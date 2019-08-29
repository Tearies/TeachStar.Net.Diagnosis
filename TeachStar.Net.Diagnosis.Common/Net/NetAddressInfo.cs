using System.Net.Sockets;

namespace TeachStar.Net.Diagnosis.Common.Net
{
    public class NetAddressInfo
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get;  set; }

        /// <summary>
        /// 网关地址
        /// </summary>
        public string GateWay { get; set; }

        /// <summary>
        /// Mac地址
        /// </summary>
        public string MacAddress { get; set; }
       
        /// <summary>
        /// 驱动名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 连接名称
        /// </summary>
        public string ConnectName { get; set; }

        /// <summary>
        /// 地址名称
        /// </summary>
        public AddressFamily AddressFamily { get; set; }

        public override string ToString()
        { 
            return $"{ConnectName}:{DeviceName}({Ip}|{MacAddress}|{GateWay})";
        }

       
    }
}