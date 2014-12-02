using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;

namespace Xamarin.Forms.Labs.WP8
{
    public static class OrientationExtensions
    {
        public static Orientation ToOrientation(this PageOrientation orientation)
        {
            return (Orientation)((int)orientation);
        }

        public static PageOrientation ToPageOrientation(this Orientation orientation)
        {
            return (PageOrientation)((int)orientation);
        }
    }
}
