using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Arpm.Command
{
    class Resolve: AbstractCommand<Option.Resolve>
    {
        public override void Execute()
        {
            var destIP = IPAddress.Parse(Option.DestIP);

            var device = Manager.GetNetworkDeviceByCode(Option.DeviceName);

            var arp = new ARP(device.Pcap);

            byte[] resultMAC;

            if (Option.LocalIP == null || Option.LocalMac == null)
            {
                Console.WriteLine("Request sent to the address {0}", destIP);

                resultMAC = arp.Resolve(destIP).GetAddressBytes();

                Console.WriteLine(
                    "Response: {0}",
                    string.Join(
                        "-",
                        (from z in resultMAC select z.ToString("X2")).ToArray()
                    )
                );
            }
            else
            {
                var localIP = IPAddress.Parse(this.Option.LocalIP);

                var localMAC = PhysicalAddress.Parse(this.Option.LocalMac);

                Console.WriteLine("Response to the address {0}", destIP);

                Console.WriteLine("Creating record {0} = {1}", localIP, localMAC);

                resultMAC = arp.Resolve(destIP, localIP, localMAC).GetAddressBytes();

                Console.WriteLine("Success");
            }
        }
    }
}
