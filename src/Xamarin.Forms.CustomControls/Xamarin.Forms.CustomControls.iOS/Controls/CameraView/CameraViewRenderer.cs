using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Drawing;
using MonoTouch.AVFoundation;
using Xamarin.Forms.CustomControls.Controls;
using Xamarin.Forms.CustomControls.iOS.Controls;

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraViewRenderer))]

namespace Xamarin.Forms.CustomControls.iOS.Controls
{
    public class CameraViewRenderer : ViewRenderer<CameraView, CameraPreview>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CameraView> e)
        {
            System.Diagnostics.Debug.WriteLine("Testing CameraView");
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                this.SetNativeControl(new CameraPreview());
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Camera":
                    break;
                default:
                    break;
            }
        }
    }

    [Register("CameraPreview")]
    public class CameraPreview : UIView
    {
        private AVCaptureVideoPreviewLayer previewLayer;

        public CameraPreview()
        {
            Initialize();
        }

        public CameraPreview(RectangleF bounds)
            : base(bounds)
        {
            Initialize();
        }

        public override void Draw(RectangleF rect)
        {
            base.Draw(rect);
            this.previewLayer.Frame = rect;
        }

        private void Initialize()
        {
            var captureSession = new MonoTouch.AVFoundation.AVCaptureSession();
            previewLayer = new MonoTouch.AVFoundation.AVCaptureVideoPreviewLayer(captureSession)
                {
                    LayerVideoGravity = MonoTouch.AVFoundation.AVLayerVideoGravity.ResizeAspectFill,
                    Frame = this.Bounds
                };

            var device = MonoTouch.AVFoundation.AVCaptureDevice.DefaultDeviceWithMediaType(
                MonoTouch.AVFoundation.AVMediaType.Video);

            if (device == null)
            {
                System.Diagnostics.Debug.WriteLine("No device detected.");
                return;
            }

            NSError error;

            var input = new MonoTouch.AVFoundation.AVCaptureDeviceInput(device, out error);

            captureSession.AddInput(input);

            this.Layer.AddSublayer(previewLayer);

            captureSession.StartRunning();
        }
    }
}