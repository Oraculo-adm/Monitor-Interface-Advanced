
using System;
using System.Collections.Generic;
using System.Timers;
using NetInterfaces;

namespace PingMonitor
{
    public class Main
    {
        private static Timer pingTimer;

        public static void Start()
        {
            pingTimer = new Timer(5000); // 5 segundos por padrão
            pingTimer.Elapsed += (s, e) => RunPingCycle();
            pingTimer.Start();

            RunPingCycle(); // Executa logo ao iniciar
        }

        private static void RunPingCycle()
        {
            InterfaceService.Refresh(); // atualiza lista de interfaces
            var interfaces = InterfaceService.GetAll();

            foreach (var iface in interfaces)
            {
                var result = PingService.TestPing(iface);
                Console.WriteLine($"[{result.Interface}] GW: {result.GatewayPing} | Google: {result.GooglePing}");
            }
        }
    }
}
