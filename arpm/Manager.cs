using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpPcap.LibPcap;
using System.Net.NetworkInformation;
using System.Net;

namespace Arpm
{
    class Manager
    {
        protected static List<NetworkDevice> NetworkDevicesList;

        public static LibPcapLiveDeviceList PcapDeviceList
        {
            get
            {
                return LibPcapLiveDeviceList.Instance;
            }
        }

        public static List<NetworkDevice> NetworkDevices
        {
            get
            {
                if (NetworkDevicesList == null)
                {
                    ScanDevice();
                }

                return NetworkDevicesList;
            }
        }

        protected static void ScanDevice()
        {
            NetworkDevicesList = new List<NetworkDevice>();

            var typeNum = new Dictionary<NetworkInterfaceType, int>();

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                string code = null;

                if (! typeNum.ContainsKey(ni.NetworkInterfaceType))
                {
                    typeNum.Add(ni.NetworkInterfaceType, 0);
                }
                else
                {
                    typeNum[ni.NetworkInterfaceType]++;
                }

                switch (ni.NetworkInterfaceType)
                {
                    case NetworkInterfaceType.Ethernet:
                        code = "eth" + typeNum[ni.NetworkInterfaceType];
                        break;

                    case NetworkInterfaceType.Wireless80211:
                        code = "wlan" + typeNum[ni.NetworkInterfaceType];
                        break;

                    case NetworkInterfaceType.Loopback:
                        code = "lo";
                        break;
                }

                if (code != null)
                {
                    var device = PcapDeviceList.FirstOrDefault(
                        item =>
                            item.Interface
                                .MacAddress
                                .GetAddressBytes()
                                .SequenceEqual(
                                    ni.GetPhysicalAddress()
                                    .GetAddressBytes()
                                )
                    );

                    if (device != null)
                    {
                        NetworkDevicesList.Add(
                            new NetworkDevice(device)
                            {
                                Code = code
                            }
                        );
                    }
                }
            }
        }

        public static NetworkDevice GetNetworkDeviceByCode(string code)
        {
            if (NetworkDevicesList == null)
            {
                ScanDevice();
            }

            return NetworkDevicesList.FirstOrDefault(i => i.Code == code);
        }

        public static string RenderMac(byte[] address)
        {
            return string.Join(
                "-",
                (from z in address select z.ToString("X2")).ToArray()
            );
        }
    }
}


