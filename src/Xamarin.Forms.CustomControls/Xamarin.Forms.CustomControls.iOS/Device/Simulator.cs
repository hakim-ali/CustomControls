using System;
using MonoTouch.UIKit;

namespace Xamarin.Forms.CustomControls
{
	/// <summary>
	/// Apple device Simulator.
	/// </summary>
	public class Simulator : AppleDevice
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Xamarin.Forms.Labs.Simulator"/> class.
		/// </summary>
		internal Simulator ()
		{
			var b = UIScreen.MainScreen.Bounds;
			var h = b.Height * UIScreen.MainScreen.Scale;
			var w = b.Width * UIScreen.MainScreen.Scale;
			var dpi = UIScreen.MainScreen.Scale * 163;
			this.Display = new Display ((int)h, (int)w, dpi, dpi);

			this.Name = this.HardwareVersion = "Simulator";
		}
	}
}

