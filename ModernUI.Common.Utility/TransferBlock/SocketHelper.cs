using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace ModernUI.Common.Utility.TransferBlock
{
    public static class SocketHelper
    {
        /// <summary>
        /// 获取本地IP地址
        /// </summary>
        /// <returns>本地IP地址</returns>
        public static IPAddress GetLocalIP()
        {
            //获取IP地址信息
            var hostEntity = Dns.GetHostEntry(Dns.GetHostName());
            //获取网卡详情
            var netAdapterlist = NetworkInterface.GetAllNetworkInterfaces().ToList();
            //检查是否为物理网卡上的IP地址
            var checkPhysics = new Func<IPAddress, bool>(ip =>
            {
                //查找对应的网卡
                var netAdapter = netAdapterlist.Find(p =>
                {
                    if (p != null)
                    {
                        //查找对应当前IP的网卡
                        var matchedUnicastAddress = p.GetIPProperties()
                            .UnicastAddresses.FirstOrDefault(n => n.Address.ToString() == ip.ToString());
                        bool matched = (matchedUnicastAddress != null);
                        return matched;
                    }

                    return false;
                });

                if (netAdapter != null)
                {
                    //是否为虚拟网卡
                    //Todo:方法过于粗糙,有待改良
                    bool isVirtual = netAdapter.Description.ToLower().Contains("virtual") ||
                                     netAdapter.Description.ToLower().Contains("虚拟");
                    if (!isVirtual)
                    {
                        return true;
                    }
                }
                return false;
            });
            var ret = hostEntity.AddressList.FirstOrDefault(p =>
            {
                bool isIPv4 = (p.AddressFamily == AddressFamily.InterNetwork);
                bool isNotVirtual = checkPhysics(p);
                return isIPv4 && isNotVirtual;
            });
            if (null == ret)
            {
                //如果无活动物理IP网卡,使用回还地址
                const string loopback = "127.0.0.1";
                ret = IPAddress.Parse(loopback);
            }
            return ret;
        }
    }
}
