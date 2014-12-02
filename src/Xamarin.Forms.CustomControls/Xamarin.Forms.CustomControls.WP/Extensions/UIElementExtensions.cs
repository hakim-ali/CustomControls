using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Xamarin.Forms.Labs.WP8
{
    public static class UIElementExtensions
    {
        public static Task StreamToJpeg(this UIElement view, Stream stream)
        {
            return Task.Run(() => view.ToBitmap().SaveJpeg(stream, (int)view.RenderSize.Width, (int)view.RenderSize.Height, 0, 100));
        }

        public static WriteableBitmap ToBitmap(this UIElement view)
        {
            return new WriteableBitmap(view, null);
        }
    }
}
