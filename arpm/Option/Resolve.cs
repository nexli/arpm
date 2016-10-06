using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpm.Option
{
    [Verb("resolve", HelpText = "Send an ARP request or response")]
    class Resolve: EmptyOption
    {
        [Option('d', "destIp", Required = true)]
        public string DestIP { get; set; }


        [Option('l', "localIp")]
        public string LocalIP { get; set; }


        [Option('m', "localMac")]
        public string LocalMac { get; set; }

        [Option('i', "interface", Required = true)]
        public string DeviceName { get; set; }
    }
}
