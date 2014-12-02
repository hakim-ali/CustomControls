using System;
using System.Globalization;

namespace Xamarin.Forms.CustomControls.Mvvm
{
    /// <summary>
    /// Converts the Xamarin Forms page navigation to our <see cref="ViewModelNavigation"/> instance.
    /// </summary>
    class NavigationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new ViewModelNavigation((INavigation)value);
        }
    }

    public class BaseView : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseView"/> class.
        /// Binds the Navigation and IsBusy property.
        /// </summary>
        public BaseView()
        {
            SetBinding(NavigationProperty, new Binding("Navigation", converter: new NavigationConverter()));
            SetBinding(IsBusyProperty, new Binding("IsBusy"));
        }
    }
}
