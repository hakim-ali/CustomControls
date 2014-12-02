using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Xamarin.Forms.CustomControls.Droid.Extensions
{
    public static class UriExtensions
    {
        public static Android.Net.Uri ToAndroidUri(this Uri uri)
        {
            return Android.Net.Uri.Parse(uri.AbsoluteUri);
        }

        public static Uri ToSystemUri(this Android.Net.Uri uri)
        {
            return new Uri(uri.ToString());
        }
    }
}