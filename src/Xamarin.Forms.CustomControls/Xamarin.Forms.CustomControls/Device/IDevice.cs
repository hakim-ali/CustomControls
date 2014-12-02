using System;
using System.Threading.Tasks;
using Xamarin.Forms.CustomControls.Services.Media;
using Xamarin.Forms.CustomControls.Services;

namespace Xamarin.Forms.CustomControls
{
    /// <summary>
    /// Abstracted device interface.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Gets Unique Id for the device.
        /// </summary>
        string Id { get; }

		/// <summary>
		/// Gets the display information for the device.
		/// </summary>
		IDisplay Display { get; }
      
        /// <summary>
        /// Gets the picture chooser.
        /// </summary>
        /// <value>The picture chooser.</value>
        IMediaPicker MediaPicker { get; }

        /// <summary>
        /// Gets the network service.
        /// </summary>
        /// <value>The network service.</value>
        INetwork Network { get; }

      
        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>The name of the device.</value>
        string Name { get; }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        string FirmwareVersion { get; }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        string HardwareVersion { get; }

        /// <summary>
        /// Gets the manufacturer.
        /// </summary>
        string Manufacturer { get; }

        /// <summary>
        /// Starts the default app associated with the URI for the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The launch operation.</returns>
        Task<bool> LaunchUriAsync(Uri uri);
    }
}

