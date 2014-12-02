﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xamarin.Forms.Platform.WinPhone;

namespace Xamarin.Forms.Labs.WP8
{
    public static class ColorExtensions
    {
        public static Brush ToBrush(this Color color)
        {
            return new SolidColorBrush(color.ToMediaColor());
        }

        public static System.Windows.Media.Color ToMediaColor(this Color color)
        {
            return System.Windows.Media.Color.FromArgb((byte)(color.A * 255.0), (byte)(color.R * 255.0), (byte)(color.G * 255.0), (byte)(color.B * 255.0));
        }
    }
}
