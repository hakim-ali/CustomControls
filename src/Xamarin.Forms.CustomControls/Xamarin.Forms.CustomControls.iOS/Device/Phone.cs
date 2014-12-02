using System;
using System.ComponentModel;
using Xamarin.Forms.Labs.iOS.Services;

namespace Xamarin.Forms.CustomControls
{
	/// <summary>
	/// Apple iPhone.
	/// </summary>
	public class Phone : AppleDevice
	{
		/// <summary>
		/// The phone type.
		/// </summary>
		public enum PhoneType
		{
			/// <summary>
			/// Unknown phone type.
			/// </summary>
			[Description("Unknown device")]
			Unknown = 0,

			/// <summary>
			/// The iPhone 1G.
			/// </summary>
			[Description("iPhone 1G")]
			iPhone1G = 1,

			[Description("iPhone 3G")]
			iPhone3G,
			[Description("iPhone 3GS")]
			iPhone3GS,
			[Description("iPhone 4 GSM")]
			iPhone4GSM,
			[Description("iPhone 4 CDMA")]
			iPhone4CDMA,
			[Description("iPhone 4S")]
			iPhone4S,
			[Description("iPhone 5 GSM")]
			iPhone5GSM,
			[Description("iPhone 5 CDMA")]
			iPhone5CDMA,
			[Description("iPhone 5C CDMA")]
			iPhone5C_CDMA,
			[Description("iPhone 5C GSM")]
			iPhone5C_GSM,
			[Description("iPhone 5S CDMA")]
			iPhone5S_CDMA,
			[Description("iPhone 5S GSM")]
			iPhone5S_GSM,
			[Description("iPhone 6")]
			iPhone6,
			[Description("iPhone 6 Plus")]
			iPhone6Plus,
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xamarin.Forms.Labs.Phone"/> class.
		/// </summary>
		/// <param name="majorVersion">Major version.</param>
		/// <param name="minorVersion">Minor version.</param>
		internal Phone(int majorVersion, int minorVersion)
			: base()
		{
		
			switch (majorVersion)
			{
			case 1:
				this.Version = minorVersion == 1 ? PhoneType.iPhone1G : PhoneType.iPhone3G;
				break;
			case 2:
				this.Version = PhoneType.iPhone3GS;
				break;
			case 3:
				this.Version = minorVersion == 1 ? PhoneType.iPhone4GSM : PhoneType.iPhone4CDMA;
				break;
			case 4:
				this.Version = PhoneType.iPhone4S;
				break;
			case 5:
				this.Version = PhoneType.iPhone5GSM + minorVersion - 1;
				break;
			case 6:
				this.Version = minorVersion == 1 ? PhoneType.iPhone5S_CDMA : PhoneType.iPhone5S_GSM;
				break;
			case 7:
				this.Version = minorVersion == 1 ? PhoneType.iPhone6Plus : PhoneType.iPhone6;
				break;
			default:
				this.Version = PhoneType.Unknown;
				break;
			}

			if (this.Version == PhoneType.iPhone6)
			{
				this.Display = new Display(1334, 750, 326, 326);
			}
			else if (this.Version == PhoneType.iPhone6Plus)
			{
				this.Display = new Display(1920, 1080, 401, 401);
			}
			else if (majorVersion > 4)
			{
				this.Display = new Display(1136, 640, 326, 326);
			}
			else if (majorVersion > 2)
			{
				this.Display = new Display(960, 640, 326, 326);
			}
			else
			{
				this.Display = new Display(480, 320, 163, 163);
			}

			this.Name = this.HardwareVersion = this.Version.GetDescription();
		}

		/// <summary>
		/// Gets the version of iPhone.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public PhoneType Version
		{
			get;
			private set;
		}
	}
}

