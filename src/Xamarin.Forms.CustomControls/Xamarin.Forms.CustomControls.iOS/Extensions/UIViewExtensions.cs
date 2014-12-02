using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Xamarin.Forms.CustomControls.iOS
{
    public static class UIViewExtensions
    {
        public static async Task StreamToPng(this UIView view, Stream stream)
        {
            var bytes = view.ToNativeImage().AsPNG().ToArray();

            await stream.WriteAsync(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Takes an image of the view.
        /// </summary>
        /// <param name="view">View to process to image.</param>
        /// <returns>A native image of type <see cref="UIImage"/>.</returns>
        public static UIImage ToNativeImage(this UIView view)
        {
            UIGraphics.BeginImageContext(view.Bounds.Size);

            view.Layer.RenderInContext(UIGraphics.GetCurrentContext());

            var image = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();

            return image;
        }

        public static CGBitmapContext ToBitmapContext(this System.Drawing.SizeF size)
        {
            using (var colorSpace = CGColorSpace.CreateDeviceRGB())
            {
                var pixelsWide = (int)size.Width;
                var pixelsHigh = (int)size.Height;

                var bitmapBytesPerRow = (pixelsWide * 4);// 1
                var bitmapByteCount = (bitmapBytesPerRow * pixelsHigh);

                var bitmapData = new byte[bitmapByteCount];

                return new CGBitmapContext(
                        bitmapData,
                        pixelsWide,
                        pixelsHigh,
                        8,      // bits per component
                        bitmapBytesPerRow,
                        colorSpace,
                        CGImageAlphaInfo.PremultipliedLast);
            }

        }
    }
}