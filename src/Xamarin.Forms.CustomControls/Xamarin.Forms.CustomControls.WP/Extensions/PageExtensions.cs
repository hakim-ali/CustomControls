using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using Xamarin.Forms.Labs.Mvvm;
using Xamarin.Forms.Labs.Services;

using XLabs.Ioc;

namespace Xamarin.Forms.Labs.WP8
{
    public static class PageExtensions
    {
        public static void SetOrientation(this PhoneApplicationPage page, PageOrientation? orientation = null)
        {
            var app = Resolver.Resolve<IXFormsApp>() as XFormsAppWP;

            if (app != null)
            {
                app.SetOrientation(orientation ?? page.Orientation);
            }
        }
    }
}
