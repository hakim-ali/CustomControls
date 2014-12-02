using Android.Telephony;
using Android.OS;
using Xamarin.Forms.Labs.Droid;
using Xamarin.Forms.Labs.Droid.Services;
using System.Threading.Tasks;
using Xamarin.Forms.CustomControls;
using Xamarin.Forms.CustomControls.Droid.Services.Media;
using Xamarin.Forms.CustomControls.Services.Media;
using Xamarin.Forms.CustomControls.Services;


namespace Xamarin.Forms.Labs
{
    /// <summary>
    /// Android device implements <see cref="IDevice"/>.
    /// </summary>
    public class AndroidDevice : IDevice
    {
        private static IDevice currentDevice;

      
        /// <summary>
        /// Prevents a default instance of the <see cref="AndroidDevice"/> class from being created. 
        /// </summary>
        private AndroidDevice()
        {
         
            this.Display = new Display();

            this.Manufacturer = Build.Manufacturer;
            this.Name = Build.Model;
            this.HardwareVersion = Build.Hardware;
            this.FirmwareVersion = Build.VERSION.Release;

         
            this.MediaPicker = new MediaPicker();

            this.Network = new Network();
        }

        /// <summary>
        /// Gets the current device.
        /// </summary>
        /// <value>
        /// The current device.
        /// </value>
        public static IDevice CurrentDevice { get { return currentDevice ?? (currentDevice = new AndroidDevice()); } }

        #region IDevice implementation
        /// <summary>
        /// Gets Unique Id for the device.
        /// </summary>
        /// <value>
        /// The id for the device.
        /// </value>
        public string Id
        {
            // TODO: Verify what is the best combination of Unique Id for Android
            get { return Build.Serial; }
        }

        /// <summary>
        /// Gets the display information for the device.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public IDisplay Display
        {
            get;
            private set;
        }

        
        /// <summary>
        /// Gets the picture chooser.
        /// </summary>
        /// <value>The picture chooser.</value>
        public IMediaPicker MediaPicker
        {
            get; 
            private set;
        }

        /// <summary>
        /// Gets the network service.
        /// </summary>
        /// <value>The network service.</value>
        public INetwork Network
        {
            get;
            private set;
        }

        
        /// <summary>
        /// Gets the default microphone for the device
        /// </summary>
        public IAudioStream Microphone
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>
        /// The name of the device.
        /// </value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        /// <value>
        /// The firmware version.
        /// </value>
        public string FirmwareVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        /// <value>
        /// The hardware version.
        /// </value>
        public string HardwareVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the manufacturer.
        /// </summary>
        /// <value>
        /// The manufacturer.
        /// </value>
        public string Manufacturer
        {
            get;
            private set;
        }

        /// <summary>
        /// Starts the default app associated with the URI for the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The launch operation.</returns>
        public Task<bool> LaunchUriAsync(System.Uri uri)
        {
            return Task.Run(() =>
                {
                    try
                    {
                        Forms.Context.StartActivity(new Android.Content.Intent("android.intent.action.VIEW", Android.Net.Uri.Parse(uri.ToString())));
                        return true;
                    }
                    catch (System.Exception ex)
                    {
                        Android.Util.Log.Error("Device.LaunchUriAsync", ex.Message);
                        return false;
                    }
                });
        }
        #endregion
    }
}

