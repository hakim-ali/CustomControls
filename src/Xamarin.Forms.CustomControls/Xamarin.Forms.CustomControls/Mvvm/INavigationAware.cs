using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.CustomControls.Mvvm
{
    public interface INavigationAware
    {
        /// <summary>
        /// Called when being navigated to.
        /// </summary>
        /// <remarks>
        /// Can be implemented on either viewmodel or view.
        /// </remarks>
        /// <param name="previousView">The view being navigated away from.</param>
        void OnNavigatingTo(Page previousView);

        /// <summary>
        /// Called when being navigated away from.
        /// </summary>
        /// <remarks>
        /// Can be implemented on either viewmodel or view.
        /// </remarks>
        /// <param name="nextView">The view being navigated to.</param>
        void OnNavigatingFrom(Page nextView);
    }
}
