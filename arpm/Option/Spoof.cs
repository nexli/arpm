using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpm.Option
{
    [Verb("spoof", HelpText = "Arp spoofing")]
    class Spoof
    {
        [Option('i', "interface", Required = true)]
        public string Interface { get; set; }


        [Option('r', "route", Required = true)]
        public string RouteIP { get; set; }


        [Option('c', "clients", Required = true)]
        public IEnumerable<string> ClientIP { get; set; }


        [Option('t', "timeSpan", Default = 500)]
        public int TimeSpan { get; set; }
    }
}
