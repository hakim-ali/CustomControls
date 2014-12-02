using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.CustomControls.Mvvm
{
    public class NavigationAwareViewModel : ViewModel, INavigationAware
    {
        /// <summary>
        /// Called when being navigated to.
        /// </summary>
        /// <param name="previousView">The view being navigated away from.</param>
        public virtual void OnNavigatingTo(Page previousView)
        {
        }

        /// <summary>
        /// Called when being navigated away from.
        /// </summary>
        /// <param name="nextView">The view being navigated to.</param>
        public virtual void OnNavigatingFrom(Page nextView)
        {
        }
    }
}
