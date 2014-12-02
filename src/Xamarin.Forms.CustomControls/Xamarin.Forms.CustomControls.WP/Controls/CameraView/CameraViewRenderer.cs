using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Devices;
using Microsoft.Xna.Framework.Media;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Controls;
using Xamarin.Forms.Labs.Mvvm;
using Xamarin.Forms.Labs.Services;
using Xamarin.Forms.Labs.WP8.Controls;
using Xamarin.Forms.Platform.WinPhone;
using XLabs.Ioc;

[assembly: ExportRenderer(typeof(CameraView), typeof(CameraViewRenderer))]

namespace Xamarin.Forms.Labs.WP8.Controls
{
    public class CameraViewRenderer : ViewRenderer<CameraView, Canvas>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CameraView> e)
        {
            base.OnElementChanged(e);

            System.Diagnostics.Debug.WriteLine(this.Parent);

            if (this.Control == null)
            {
                
                // TODO: determine how to dispose the camera...
                var camera = new PhotoCamera((CameraType)((int)e.NewElement.Camera));

                var app = Resolver.Resolve<IXFormsApp>();

                var rotation = camera.Orientation;
                switch (app.Orientation)
                {
                    case Orientation.LandscapeLeft:
                        rotation -= 90;
                        break;
                    case Orientation.LandscapeRight:
                        rotation += 90;
                        break;
                }

                var videoBrush = new VideoBrush()
                {
                    RelativeTransform = new CompositeTransform()
                    {
                        CenterX = 0.5,
                        CenterY = 0.5,
                        Rotation = rotation
                    }
                };
                
                
                videoBrush.SetSource(camera);

                var canvas = new Canvas()
                {
                    Background = videoBrush
                };

                this.SetNativeControl(canvas);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Camera":
                    var brush = this.Control.Background as VideoBrush;
                    var camera = new PhotoCamera((CameraType)((int)this.Element.Camera));
                    brush.SetSource(camera);
                    break;
                default:
                    break;
            }
        }
    }
}
