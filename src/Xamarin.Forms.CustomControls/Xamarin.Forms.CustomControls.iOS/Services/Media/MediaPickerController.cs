using System;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using Xamarin.Forms.CustomControls.Services.Media;

namespace Xamarin.Forms.Labs.iOS.Services.Media
{
	public sealed class MediaPickerController
		: UIImagePickerController
	{
		internal MediaPickerController (MediaPickerDelegate mpDelegate)
		{
			base.Delegate = mpDelegate;
		}

		public override MonoTouch.Foundation.NSObject Delegate
		{
			get { return base.Delegate; }
			set { throw new NotSupportedException(); }
		}

		public Task<MediaFile> GetResultAsync()
		{
			return ((MediaPickerDelegate)Delegate).Task;
		}
	}
}

