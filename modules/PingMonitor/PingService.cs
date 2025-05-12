
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using NetInterfaces;

namespace PingMonitor
{
    public class PingResult
    {
        public string Interface { get; set; }
        public string IP { get; set; }
        public string GatewayPing { get; set; }
        public string GooglePing { get; set; }
    }

    public static class PingService
    {
        public static PingResult TestPing(InterfaceData iface)
        {
            var result = new PingResult
            {
                Interface = iface.Name,
                IP = iface.IPv4,
                GatewayPing = PingHost(iface.Gateway),
                GooglePing = PingHost("8.8.8.8", iface.IPv4)
            };

            return result;
        }

        private static string PingHost(string host, string sourceIp = null)
        {
            try
            {
                using var ping = new Ping();
                var options = new PingOptions { DontFragment = true };
                byte[] buffer = new byte[32];
                int timeout = 2000;

                PingReply reply = sourceIp != null
                    ? ping.Send(host, timeout, buffer, options)
                    : ping.Send(host, timeout);

                if (reply.Status == IPStatus.Success)
                    return $"{reply.RoundtripTime} ms";
                else
                    return "❌";
            }
            catch (Exception ex)
            {
                return "Erro";
            }
        }
    }
}
