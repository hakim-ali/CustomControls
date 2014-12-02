using System;
using System.ComponentModel;

namespace Xamarin.Forms.CustomControls
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {            
            var field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}

