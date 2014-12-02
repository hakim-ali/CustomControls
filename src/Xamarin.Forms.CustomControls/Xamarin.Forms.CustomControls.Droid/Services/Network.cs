using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using Android.Net;
using Xamarin.Forms.CustomControls.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin.Forms.Labs.Droid.Services.Network))]

namespace Xamarin.Forms.Labs.Droid.Services
{
	public class Network : INetwork
    {
		public Network()
		{
			/* TODO: reachability changed */
		}

		public event Action<NetworkStatus> ReachabilityChanged;

		public NetworkStatus InternetConnectionStatus ()
		{
			NetworkStatus status = NetworkStatus.NotReachable;

			ConnectivityManager cm = (ConnectivityManager) Application.Context.GetSystemService(Context.ConnectivityService);
			NetworkInfo ni = cm.ActiveNetworkInfo;

			if (ni.TypeName.ToUpper ().Contains ("WIFI")
			    && ni.IsConnectedOrConnecting)
				status = NetworkStatus.ReachableViaWiFiNetwork;

			if (ni.TypeName.ToUpper ().Contains ("MOBILE")
				&& ni.IsConnectedOrConnecting)
				status = NetworkStatus.ReachableViaCarrierDataNetwork;

			return status;
		}

        public Task<bool> IsReachable(string host, TimeSpan timeout)
        {
            return Task.Run(() =>
            {
                try
                {
                    var address = InetAddress.GetByName(host);

                    return address != null;// && (address.IsReachable((int)timeout.TotalMilliseconds) || );
                }
                catch (Java.Net.UnknownHostException)
                {
                    return false;
                }
            });
        }

//        public bool CanPing(string host)
//        {
//            Process p1 = Java.Lang.Runtime.GetRuntime().Exec(string.Format("ping -c 1 {0}", host));
//
//
//            int returnVal = p1.();
//            boolean reachable = (returnVal==0);
//        }

        public async Task<bool> IsReachableByWifi(string host, TimeSpan timeout)
        {
            return this.InternetConnectionStatus() == NetworkStatus.ReachableViaWiFiNetwork && await this.IsReachable(host, timeout);
        }
    }
}