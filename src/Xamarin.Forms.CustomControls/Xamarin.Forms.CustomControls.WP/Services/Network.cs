using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Microsoft.Phone.Net.NetworkInformation;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin.Forms.Labs.WP8.Services.Network))]

namespace Xamarin.Forms.Labs.WP8.Services
{
    public class Network : INetwork
    {
        private event Action<NetworkStatus> reachabilityChanged;

        private NetworkStatus networkStatus;

        public Network()
        {
            this.networkStatus = InternetConnectionStatus();
        }

        public event Action<NetworkStatus> ReachabilityChanged
        {
            add
            {
                if (this.reachabilityChanged == null)
                {
                    DeviceNetworkInformation.NetworkAvailabilityChanged += DeviceNetworkInformation_NetworkAvailabilityChanged;
                }

                this.reachabilityChanged += value;
            }

            remove
            {
                this.reachabilityChanged -= value;

                if (this.reachabilityChanged == null)
                {
                    DeviceNetworkInformation.NetworkAvailabilityChanged -= DeviceNetworkInformation_NetworkAvailabilityChanged;
                }
            }
        }

        public NetworkStatus InternetConnectionStatus()
        {
            if (DeviceNetworkInformation.IsNetworkAvailable)
            {
                if (DeviceNetworkInformation.IsWiFiEnabled && Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    return NetworkStatus.ReachableViaWiFiNetwork;
                }

                if (Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType == NetworkInterfaceType.MobileBroadbandCdma || Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType == NetworkInterfaceType.MobileBroadbandGsm)
                {
                    return NetworkStatus.ReachableViaCarrierDataNetwork;
                }
            }

            return NetworkStatus.NotReachable;
        }

        public Task<bool> IsReachable(string host, TimeSpan timeout)
        {
            return Task<bool>.Run(() =>
                {
                    if (!DeviceNetworkInformation.IsNetworkAvailable)
                    {
                        return false;
                    }

                    AutoResetEvent e = new AutoResetEvent(false);

                    bool isReachable = false;
                    NameResolutionCallback d = delegate(NameResolutionResult result) 
                    {
                        isReachable = result.NetworkErrorCode == NetworkError.Success;
                        e.Set();
                    };

                    DeviceNetworkInformation.ResolveHostNameAsync(new System.Net.DnsEndPoint(host, 0), d, this);


                    e.WaitOne(timeout);

                    return isReachable;
                });
        }

        public async Task<bool> IsReachableByWifi(string host, TimeSpan timeout)
        {
            return (InternetConnectionStatus() == NetworkStatus.ReachableViaWiFiNetwork  &&
                await this.IsReachable(host, timeout));
        }

        void DeviceNetworkInformation_NetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            var status = this.InternetConnectionStatus();

            if (status == this.networkStatus)
            {
                return;
            }

            var handler = this.reachabilityChanged;

            if (handler != null)
            {
                handler(status);
            }
        }

    }
}
