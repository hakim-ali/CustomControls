using System;
using Xamarin.Forms;
using Xamarin.Forms.Labs;
using MonoTouch.UIKit;
using Xamarin.Forms.Labs.iOS;
using Xamarin.Forms.Platform.iOS;
using System.Reflection;
using Xamarin.Forms.CustomControls;
using Xamarin.Forms.CustomControls.iOS;

[assembly: ExportRenderer(typeof(ExtendedMasterDetailPage), typeof(ExtendedPhoneMasterDetailPageRenderer), UIUserInterfaceIdiom.Phone)]
[assembly: ExportRenderer(typeof(ExtendedMasterDetailPage), typeof(ExtendedTabletMasterDetailPageRenderer), UIUserInterfaceIdiom.Pad)]

namespace Xamarin.Forms.CustomControls.iOS
{
    public class ExtendedTabletMasterDetailPageRenderer : TabletMasterDetailRenderer
    {
        public static Func<bool> ShouldHideMenu;

        public ExtendedTabletMasterDetailPageRenderer()
        {
            var version = new Version(MonoTouch.Constants.Version);
            if (version >= new Version(8, 0))
            {
                // Code that uses features from Xamarin.iOS 7.0
            }
            else
            {
                var fi = typeof(TabletMasterDetailRenderer).GetField("innerDelegate", BindingFlags.NonPublic | BindingFlags.Instance);
                var d = fi.GetValue(this) as UISplitViewControllerDelegate;
                this.Delegate = new ExtendedDelegate(d);
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            var version = new Version(MonoTouch.Constants.Version);
            if (version >= new Version(8, 0))
            {
                this.PreferredDisplayMode = ShouldHide ?
                    UISplitViewControllerDisplayMode.PrimaryHidden :
                    UISplitViewControllerDisplayMode.Automatic;
            }
        }

        private static bool ShouldHide
        {
            get
            {
                return ShouldHideMenu == null ? true : ShouldHideMenu();
            }
        }

        private class ExtendedDelegate : UISplitViewControllerDelegate
        {
            private readonly PropertyInfo buttonInfo;
            private readonly object baseDelegate;

            public ExtendedDelegate(object baseDelegate)
            {
                this.baseDelegate = baseDelegate;
                this.buttonInfo = baseDelegate.GetType().GetProperty("PresentButton");
            }

            public UIBarButtonItem PresentButton
            {
                get
                {
                    return buttonInfo.GetValue(this.baseDelegate) as UIBarButtonItem;
                }
                set
                {
                    buttonInfo.SetValue(this.baseDelegate, value);
                }
            }


            public override void WillShowViewController(UISplitViewController svc, UIViewController aViewController, UIBarButtonItem button)
            {
                this.PresentButton = (UIBarButtonItem)null;
            }

            public override void WillHideViewController(UISplitViewController svc, UIViewController aViewController, UIBarButtonItem barButtonItem, UIPopoverController pc)
            {
                this.PresentButton = barButtonItem;
            }

            public override bool ShouldHideViewController(UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
            {
                return ShouldHide;
            }
        }
    }

    public class ExtendedPhoneMasterDetailPageRenderer : PhoneMasterDetailRenderer
    {
        public ExtendedPhoneMasterDetailPageRenderer()
        {
        }
    }
}

