using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.CustomControls.Mvvm
{
    public class NavigationView : NavigationPage
    {
        private const string CurrentPagePropertyName = "CurrentPage";

        private Page _previousPage;
        private Page _mainPage;

        public NavigationView()
        {
        }

        public NavigationView(Page root)
            : base(root)
        {
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            Page view = (Page)child;

            if (_mainPage == null)
            {
                _mainPage = view;
            }

            // Since OnChildRemoved event is not triggered for main page.
            if (CurrentPage == _mainPage)
            {
                OnNavigatingFrom(_mainPage, view);
            }

            OnNavigatingTo(view, CurrentPage);
        }

        protected override void OnChildRemoved(Element child)
        {
            base.OnChildRemoved(child);

            Page view = (Page)child;

            OnNavigatingFrom(view, _previousPage);

            // Since OnChildAdded is not triggered for main page.
            if (_previousPage == _mainPage)
            {
                OnNavigatingTo(_mainPage, view);
            }
        }

        protected override void OnPropertyChanging(string propertyName = null)
        {
            if (propertyName == CurrentPagePropertyName)
            {
                _previousPage = CurrentPage;
            }

            base.OnPropertyChanging(propertyName);
        }

        private INavigationAware AsNavigationAware(VisualElement element)
        {
            var navigationAware = element.BindingContext as INavigationAware;
            if (navigationAware == null)
            {
                navigationAware = element as INavigationAware;
            }

            return navigationAware;
        }

        protected void OnNavigatingTo(Page targetView, Page previousView)
        {
            Debug.WriteLine("OnNavigatingTo: targetView={0}, nextView={1}", targetView.GetType().Name, previousView != null ? previousView.GetType().Name : string.Empty);

            var navigationAware = AsNavigationAware(targetView);
            if (navigationAware != null)
            {
                navigationAware.OnNavigatingTo(previousView);
            }
        }

        protected void OnNavigatingFrom(Page targetView, Page nextView)
        {
            Debug.WriteLine("OnNavigatingFrom: targetView={0}, previousView={1}", targetView.GetType().Name, nextView.GetType().Name);

            var navigationAware = AsNavigationAware(targetView);
            if (navigationAware != null)
            {
                navigationAware.OnNavigatingFrom(nextView);
            }
        }
    }
}
