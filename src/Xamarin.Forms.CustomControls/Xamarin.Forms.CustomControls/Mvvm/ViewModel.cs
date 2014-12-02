using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using Xamarin.Forms.CustomControls.Data;

namespace Xamarin.Forms.CustomControls.Mvvm
{
    /// <summary>
    /// View model base class.
    /// </summary>
    /// <example>
    /// To implement observable property:
    /// private object propertyBackField;
    /// public object Property
    /// {
    /// get { return this.propertyBackField; }
    /// set
    /// {
    /// this.ChangeAndNotify(ref this.propertyBackField, value);
    /// }
    /// </example>
    public abstract class ViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the navigation.
        /// </summary>
        /// <value>The navigation.</value>
        public ViewModelNavigation Navigation { get; set; }

        private bool _isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty<bool>(ref _isBusy, value);
            }
        }

        #region Protected methods
        /// <summary>
        /// Changes the property if the value is different and invokes PropertyChangedEventHandler.
        /// </summary>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        /// <param name="property">Property.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Obsolete("Use the SetProperty method instead.")]
        protected bool ChangeAndNotify<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            return SetProperty(ref property, value, propertyName);
        }

        #endregion
    }
}

