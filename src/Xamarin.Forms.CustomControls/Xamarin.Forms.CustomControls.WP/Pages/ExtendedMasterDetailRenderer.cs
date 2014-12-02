using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.WP8.Pages;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(ExtendedMasterDetailPage), typeof(ExtendedMasterDetailRenderer))]

namespace Xamarin.Forms.Labs.WP8.Pages
{
    public class ExtendedMasterDetailRenderer : MasterDetailRenderer
    {
    }
}
