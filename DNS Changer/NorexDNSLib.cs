using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace DNS_Changer
{
    public static class NorexDNSLib
    {
        /// <summary>
        /// get all network interfaces from windows.
        /// </summary>
        /// <returns>a list of all Network Iterfaces</returns>
        public static List<NetworkInterface> GetAllNetworkInterfaces()
        {
            List<NetworkInterface> nics = new List<NetworkInterface>();

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up) // Only include active/connected NICs
                    nics.Add(adapter);
            }

            return nics;
        }
        /// <summary>
        /// get the active network interface 
        /// </summary>
        /// <returns> the active network</returns>
        public static NetworkInterface GetActiveEthernetOrWifiNetworkInterface()
        {
            var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                a => a.OperationalStatus == OperationalStatus.Up &&
                (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));

            return Nic;
        }
        /// <summary>
        /// Change the windows dns
        /// </summary>
        /// <param name="nicName">Name of network interface</param>
        /// <param name="dnsAddresses">a string array of Primary and secondary dns servers</param>
        public static void SetDnsServers(string nicName, string[] dnsAddresses)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] && mo["Caption"].ToString().Contains(nicName))
                {
                    ManagementBaseObject dnsClientConfig = mo.GetMethodParameters("SetDNSServerSearchOrder");

                    dnsClientConfig["DNSServerSearchOrder"] = dnsAddresses;

                    // Invoke the method to set the DNS server addresses
                    object result = mo.InvokeMethod("SetDNSServerSearchOrder", dnsClientConfig, null);

                    // If successful, display a message box indicating success
                    if ((uint)result == 0)
                        MessageBox.Show($"DNS servers changed to {string.Join(", ", dnsAddresses)} for network adapter '{nicName}'", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return; // Exit loop after setting first matching network adapter's configuration.
                }
            }

            // Display error message if no matching network adapters found.
            MessageBox.Show($"No active network adapters found with name '{nicName}'. Please check your input and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// disable dns of a network interface
        /// </summary>
        /// <param name="nicName">the name of network interface</param>
        public static void DisableDns(string nicName)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] && mo["Caption"].ToString().Contains(nicName))
                {
                    // Set the DNS server search order to an empty array
                    string[] dnsAddresses = { };
                    ManagementBaseObject dnsClientConfig = mo.GetMethodParameters("SetDNSServerSearchOrder");
                    dnsClientConfig["DNSServerSearchOrder"] = dnsAddresses;

                    // Invoke the method to set the DNS server addresses
                    object result = mo.InvokeMethod("SetDNSServerSearchOrder", dnsClientConfig, null);

                    // If successful, display a message box indicating success and return.
                    if ((uint)result == 0)
                        MessageBox.Show($"DNS servers disabled for network adapter '{nicName}'", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
            }

            // Display error message if no matching network adapters found.
            MessageBox.Show($"No active network adapters found with name '{nicName}'. Please check your input and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
