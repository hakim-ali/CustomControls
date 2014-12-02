using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Xamarin.Forms.Labs.WP8
{
    public static class DeviceExtensions
    {
        public static Task<bool> DriveTo(this IDevice device, Position position)
        {
            return device.LaunchUriAsync(position.DriveToLink());
        }
    }
}
