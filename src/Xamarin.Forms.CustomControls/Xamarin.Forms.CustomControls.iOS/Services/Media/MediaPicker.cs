using System;
using System.Threading.Tasks;
using MonoTouch.UIKit;
using System.Threading;
using System.IO;
using System.Linq;
using System.Drawing;
using Xamarin.Forms;
using ELCPicker;
using MonoTouch.AssetsLibrary;
using System.Collections.Generic;
using Xamarin.Forms.CustomControls.Services.Media;

[assembly: Dependency(typeof(Xamarin.Forms.Labs.iOS.Services.Media.MediaPicker))]
namespace Xamarin.Forms.Labs.iOS.Services.Media
{
	public class MediaPicker : IMediaPicker
	{
		public MediaPicker ()
		{

			IsCameraAvailable = UIImagePickerController.IsSourceTypeAvailable (UIImagePickerControllerSourceType.Camera);

			string[] availableCameraMedia = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera) ?? new string[0];
			string[] avaialbleLibraryMedia = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary) ?? new string[0];

			foreach (string type in availableCameraMedia.Concat (avaialbleLibraryMedia))
			{
				if (type == TypeMovie)
					IsVideosSupported = true;
				else if (type == TypeImage)
					IsPhotosSupported = true;
			}
		}

		public bool IsCameraAvailable
		{
			get;
			private set;
		}

		public bool IsPhotosSupported
		{
			get;
			private set;
		}

		public bool IsVideosSupported
		{
			get;
			private set;
		}

		/// <summary>
		/// Select a picture from library.
		/// </summary>
		/// <param name="options">The storage options.</param>
		/// <returns>Task&lt;IMediaFile&gt;.</returns>
		public Task<MediaFile> SelectPhotoAsync(CameraMediaStorageOptions options)
		{
			if (!IsPhotosSupported)
				throw new NotSupportedException();

			return GetMediaAsync (UIImagePickerControllerSourceType.PhotoLibrary, TypeImage);


		}

		/// <summary>
		/// Select a multi picture from library.
		/// </summary>
		/// <param name="options">The storage options.</param>
		/// <returns>Task&lt;IMediaFile&gt;.</returns>
		public Task<List<MediaFile>> SelectMultiPhotoAsync(CameraMediaStorageOptions options)
		{
			if (!IsPhotosSupported)
				throw new NotSupportedException ();

			return GetPickMediaAsync (UIImagePickerControllerSourceType.PhotoLibrary, TypeImage);
		}

		/// <summary>
		/// Takes the picture.
		/// </summary>
		/// <param name="options">The storage options.</param>
		/// <returns>Task&lt;IMediaFile&gt;.</returns>
		public Task<MediaFile> TakePhotoAsync(CameraMediaStorageOptions options){
			if (!IsPhotosSupported)
				throw new NotSupportedException();
			if (!IsCameraAvailable)
				throw new NotSupportedException();

			VerifyCameraOptions (options);

			return GetMediaAsync (UIImagePickerControllerSourceType.Camera, TypeImage, options);

		}

		/// <summary>
		/// Selects the video asynchronous.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns>Task&lt;IMediaFile&gt;.</returns>
		public Task<MediaFile> SelectVideoAsync(VideoMediaStorageOptions options){
		
			if (!IsPhotosSupported)
				throw new NotSupportedException();

			return GetMediaAsync (UIImagePickerControllerSourceType.PhotoLibrary, TypeMovie);

		}

		/// <summary>
		/// Selects the multiple video asynchronous.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns>Task&lt;IMediaFile&gt;.</returns>
		public Task<List<MediaFile>> SelectMultiVideoAsync(VideoMediaStorageOptions options){

			if (!IsPhotosSupported)
				throw new NotSupportedException();

			return GetPickMediaAsync (UIImagePickerControllerSourceType.PhotoLibrary, TypeMovie);
		}



		/// <summary>
		/// Takes the video asynchronous.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <returns>Task&lt;IMediaFile&gt;.</returns>
		public Task<MediaFile> TakeVideoAsync(VideoMediaStorageOptions options)
		{
			if (!IsVideosSupported)
				throw new NotSupportedException();
			if (!IsCameraAvailable)
				throw new NotSupportedException();

			//VerifyCameraOptions (options);

			return GetMediaAsync (UIImagePickerControllerSourceType.Camera, TypeMovie, options);
		}

		private Task<List<MediaFile>> GetPickMediaAsync (UIImagePickerControllerSourceType sourceType, string mediaType, MediaStorageOptions options = null)
		{
			UIWindow window = UIApplication.SharedApplication.KeyWindow;
			if (window == null)
				throw new InvalidOperationException ("There's no current active window");

			string TypeImage = "public.image";
			const string TypeMovie = "public.movie";
			UIViewController viewController = window.RootViewController;

			if (mediaType == TypeImage) {
				var picker = ELCImagePickerViewController.Instance (ALAssetsFilter.AllPhotos);
				viewController.PresentViewController (picker, true, null);
				return picker.Completion.ContinueWith (t => {
					//Interlocked.Exchange (ref this.pickerDelegate, null);
					viewController.DismissViewController(true,null);
					return t;

				}).Unwrap ();
			} else if (mediaType == TypeMovie) {
				var picker = ELCImagePickerViewController.Instance (ALAssetsFilter.AllVideos);
				viewController.PresentViewController (picker, true, null);
				return picker.Completion.ContinueWith (t => {
					Interlocked.Exchange (ref this.pickerDelegate, null);
					return t;
				}).Unwrap ();

			}
			return null;



//			PresentViewController (picker, true, null);
//			return ndelegate.Task.ContinueWith (t => {
//				if (this.popover != null) {
//					this.popover.Dispose();
//					this.popover = null;
//				}
//
//				Interlocked.Exchange (ref this.pickerDelegate, null);
//				return t;
//			}).Unwrap();
		}



		private Task<MediaFile> GetMediaAsync (UIImagePickerControllerSourceType sourceType, string mediaType, MediaStorageOptions options = null)
		{
			UIWindow window = UIApplication.SharedApplication.KeyWindow;
			if (window == null)
				throw new InvalidOperationException ("There's no current active window");

			UIViewController viewController = window.RootViewController;

			if (viewController == null) {
				window = UIApplication.SharedApplication.Windows.OrderByDescending (w => w.WindowLevel).FirstOrDefault (w => w.RootViewController != null);
				if (window == null)
					throw new InvalidOperationException ("Could not find current view controller");
				else
					viewController = window.RootViewController;	
			}

			while (viewController.PresentedViewController != null)
				viewController = viewController.PresentedViewController;

			MediaPickerDelegate ndelegate = new MediaPickerDelegate (viewController, sourceType, options);
			var od = Interlocked.CompareExchange (ref this.pickerDelegate, ndelegate, null);
			if (od != null)
				throw new InvalidOperationException ("Only one operation can be active at at time");

			var picker = SetupController (ndelegate, sourceType, mediaType, options);

			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad && sourceType == UIImagePickerControllerSourceType.PhotoLibrary) {	
				ndelegate.Popover = new UIPopoverController (picker);
				ndelegate.Popover.Delegate = new MediaPickerPopoverDelegate (ndelegate, picker);
				ndelegate.DisplayPopover();
			} else
				viewController.PresentViewController (picker, true, null);

			return ndelegate.Task.ContinueWith (t => {
				if (this.popover != null) {
					this.popover.Dispose();
					this.popover = null;
				}

				Interlocked.Exchange (ref this.pickerDelegate, null);
				return t;
			}).Unwrap();
		}

		/// <summary>
		/// Event the fires when media has been selected
		/// </summary>
		/// <value>The on photo selected.</value>
		public EventHandler<MediaPickerArgs> OnMediaSelected { get; set; }

		/// <summary>
		/// Gets or sets the on error.
		/// </summary>
		/// <value>The on error.</value>
		public EventHandler<MediaPickerErrorArgs> OnError { get; set; }

		private UIPopoverController popover;
		private UIImagePickerControllerDelegate pickerDelegate;
		internal const string TypeImage = "public.image";
		internal const string TypeMovie = "public.movie";

		private static MediaPickerController SetupController (MediaPickerDelegate mpDelegate, UIImagePickerControllerSourceType sourceType, string mediaType, MediaStorageOptions options = null)
		{
			var picker = new MediaPickerController (mpDelegate);
			picker.MediaTypes = new[] { mediaType };
			picker.SourceType = sourceType;

			if (sourceType == UIImagePickerControllerSourceType.Camera) {
				picker.CameraDevice = GetUICameraDevice ((options as CameraMediaStorageOptions) .DefaultCamera);

				if (mediaType == TypeImage)
					picker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Photo;
				else if (mediaType == TypeMovie) {
					VideoMediaStorageOptions voptions = (VideoMediaStorageOptions)options;

					picker.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Video;
					picker.VideoQuality = GetQuailty (voptions.Quality);
					picker.VideoMaximumDuration = voptions.DesiredLength.TotalSeconds;
				}
			}

			return picker;
		}

		private static UIImagePickerControllerCameraDevice GetUICameraDevice (CameraDevice device)
		{
			switch (device) {
			case CameraDevice.Front:
				return UIImagePickerControllerCameraDevice.Front;
			case CameraDevice.Rear:
				return UIImagePickerControllerCameraDevice.Rear;
			default:
				throw new NotSupportedException();
			}
		}

		private static UIImagePickerControllerQualityType GetQuailty (VideoQuality quality)
		{
			switch (quality) {
			case VideoQuality.Low:
				return UIImagePickerControllerQualityType.Low;
			case VideoQuality.Medium:
				return UIImagePickerControllerQualityType.Medium;
			default:
				return UIImagePickerControllerQualityType.High;
			}
		}

		private void VerifyOptions (MediaStorageOptions options)
		{
			if (options == null)
				throw new ArgumentNullException ("options");
			if (options.Directory != null && Path.IsPathRooted (options.Directory))
				throw new ArgumentException ("options.Directory must be a relative path", "options");
		}

		private void VerifyCameraOptions (CameraMediaStorageOptions options)
		{
			VerifyOptions (options);
			if (!Enum.IsDefined (typeof(CameraDevice), options.DefaultCamera))
				throw new ArgumentException ("options.Camera is not a member of CameraDevice");
		}
	


	}
}

