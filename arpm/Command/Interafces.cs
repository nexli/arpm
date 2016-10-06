using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpm.Command
{
    class Interafces : AbstractCommand<Option.Interafces>
    {
        public override void Execute()
        {
            var indentDefault = Indent(3);

            var i = 0;

            foreach (var code in Manager.NetworkDevices)
            {
                i++;

                if (i != 0)
                {
                    Console.WriteLine();
                }

                Console.WriteLine("{0}. {1}", i, code.Code);

                Console.WriteLine(indentDefault + "Name: {0}", code.Pcap.Interface.FriendlyName);

                Console.WriteLine(indentDefault + "Mac Address: {0}", Manager.RenderMac(code.MacAddress.GetAddressBytes()));

                Console.WriteLine(indentDefault + "Gateway Address: {0}", code.Pcap.Interface.GatewayAddress);

                Console.WriteLine(indentDefault + "Description: {0}", code.Pcap.Interface.Description);

                Console.WriteLine(indentDefault + "Address:");

                var addi = 0;

                foreach (var j in code.Pcap.Interface.Addresses)
                {
                    if (j.Addr != null && j.Addr.ipAddress != null && j.Netmask != null && j.Netmask.ipAddress != null)
                    {
                        addi++;

                        Console.WriteLine(Indent(6) + "{0}. ip: {1}; mask: {2}", addi, j.Addr.ipAddress, j.Netmask.ipAddress);
                    }
                    else if (j.Addr != null && j.Addr.ipAddress != null)
                    {
                        addi++;

                        Console.WriteLine(Indent(6) + "{0}. ip: {1}", addi, j.Addr.ipAddress);
                    }
                }
            }
        }

        public static string Indent(int count)
        {
            return "".PadLeft(count);
        }
    }
}
