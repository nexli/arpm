using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Arpm
{
    class NetworkDevice
    {
        protected LibPcapLiveDevice pcap;

        public LibPcapLiveDevice Pcap
        {
            get
            {
                return pcap;
            }
        }

        public string Code { get; set; }

        public PhysicalAddress MacAddress
        {
            get
            {
                return pcap.Interface.MacAddress;
            }
        }

        public NetworkDevice(LibPcapLiveDevice device)
        {
            pcap = device;
        }
    }
}
