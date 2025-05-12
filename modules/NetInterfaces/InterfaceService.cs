
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace NetInterfaces
{
    public class InterfaceData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MAC { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
        public string Subnet { get; set; }
        public string Gateway { get; set; }
        public string Type { get; set; }
    }

    public static class InterfaceService
    {
        private static List<InterfaceData> interfaces = new();

        public static void Initialize()
        {
            Refresh();
        }

        public static void Refresh()
        {
            interfaces.Clear();
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus != OperationalStatus.Up)
                    continue;

                var props = nic.GetIPProperties();
                var ipv4 = props.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
                var ipv6 = props.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetworkV6);
                var gateway = props.GatewayAddresses.FirstOrDefault()?.Address?.ToString();

                interfaces.Add(new InterfaceData
                {
                    Name = nic.Name,
                    Description = nic.Description,
                    MAC = string.Join("-", nic.GetPhysicalAddress().GetAddressBytes().Select(b => b.ToString("X2"))),
                    IPv4 = ipv4?.Address?.ToString() ?? "-",
                    IPv6 = ipv6?.Address?.ToString() ?? "-",
                    Subnet = ipv4?.IPv4Mask?.ToString() ?? "-",
                    Gateway = gateway ?? "-",
                    Type = nic.NetworkInterfaceType.ToString()
                });
            }
        }

        public static List<InterfaceData> GetAll()
        {
            return interfaces;
        }

        public static InterfaceData? GetByName(string name)
        {
            return interfaces.FirstOrDefault(i => i.Name == name);
        }

        public static InterfaceData? GetByIPv4(string ip)
        {
            return interfaces.FirstOrDefault(i => i.IPv4 == ip);
        }

        public static InterfaceData? GetByGateway(string gateway)
        {
            return interfaces.FirstOrDefault(i => i.Gateway == gateway);
        }
    }
}
