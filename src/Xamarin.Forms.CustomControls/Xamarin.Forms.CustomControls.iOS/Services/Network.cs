using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms.CustomControls.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin.Forms.Labs.iOS.Services.Network))]

namespace Xamarin.Forms.Labs.iOS.Services
{
    public class Network : INetwork
    {
        private event Action<NetworkStatus> reachabilityChanged;

        public Network()
        {

        }

        void HandleReachabilityChanged (object sender, EventArgs e)
        {
            var reachabilityChanged = this.reachabilityChanged;
            if (reachabilityChanged != null)
                reachabilityChanged(this.InternetConnectionStatus());
        }

        public event Action<NetworkStatus> ReachabilityChanged
        {
            add
            {
                if (this.reachabilityChanged == null)
                {
                    // TODO: check if this actually works
                    Reachability.ReachabilityChanged += HandleReachabilityChanged;
                }

                this.reachabilityChanged += value;
            }

            remove
            {
                this.reachabilityChanged -= value;

                if (this.reachabilityChanged == null)
                {
                    Reachability.ReachabilityChanged -= HandleReachabilityChanged;
                }
            }
        }

        public NetworkStatus InternetConnectionStatus()
        {
            return Reachability.InternetConnectionStatus();
        }

        public Task<bool> IsReachable(string host, TimeSpan timeout)
        {
            return Task<bool>.Run(() => Reachability.IsHostReachable(host));
        }

        public Task<bool> IsReachableByWifi(string host, TimeSpan timeout)
        {
            return Task<bool>.Run(() => Reachability.RemoteHostStatus(host) == NetworkStatus.ReachableViaWiFiNetwork);
        }
    }
}