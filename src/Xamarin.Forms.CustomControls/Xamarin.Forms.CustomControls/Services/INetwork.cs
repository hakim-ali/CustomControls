using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.CustomControls.Services
{
    public interface INetwork
    {
        Task<bool> IsReachable(string host, TimeSpan timeout);
        Task<bool> IsReachableByWifi(string host, TimeSpan timeout);
        NetworkStatus InternetConnectionStatus ();
        event Action<NetworkStatus> ReachabilityChanged;
    }
}
