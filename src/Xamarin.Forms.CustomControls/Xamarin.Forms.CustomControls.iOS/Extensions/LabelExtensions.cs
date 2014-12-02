using System;
using Xamarin.Forms.Platform.iOS;

namespace Xamarin.Forms.CustomControls.iOS
{
    public static class LabelExtensions
    {
        public static void AdjustHeight(this Label label)
        {
            label.HeightRequest = label.Text.StringHeight(label.Font.ToUIFont(), (float)label.Width);
        }
    }
}

