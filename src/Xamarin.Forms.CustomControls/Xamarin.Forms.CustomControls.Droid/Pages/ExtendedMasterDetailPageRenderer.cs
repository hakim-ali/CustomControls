using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.CustomControls;
using Xamarin.Forms.CustomControls.Droid.Pages;

[assembly: ExportRenderer(typeof(ExtendedMasterDetailPage), typeof(ExtendedMasterDetailRenderer))]

namespace Xamarin.Forms.CustomControls.Droid.Pages
{
    public class ExtendedMasterDetailRenderer : MasterDetailRenderer
    {
    }
}
