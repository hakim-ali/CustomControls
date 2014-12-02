
using XLabs.Ioc;
using System.Diagnostics;
using Xamarin.Forms.CustomControls.Mvvm;

namespace Xamarin.Forms.Labs.Sample
{
    /// <summary>
    /// Class App.
    /// </summary>
    public class App
    {
        /// <summary>
        /// Initializes the application.
        /// </summary>
        public static void Init()
        {

            var app = Resolver.Resolve<IXFormsApp>();
            if (app == null)
            {
                return;
            }

            app.Closing += (o, e) => Debug.WriteLine("Application Closing");
            app.Error += (o, e) => Debug.WriteLine("Application Error");
            app.Initialize += (o, e) => Debug.WriteLine("Application Initialized");
            app.Resumed += (o, e) => Debug.WriteLine("Application Resumed");
            app.Rotation += (o, e) => Debug.WriteLine("Application Rotated");
            app.Startup += (o, e) => Debug.WriteLine("Application Startup");
            app.Suspended += (o, e) => Debug.WriteLine("Application Suspended");
        }

        /// <summary>
        /// Gets the main page.
        /// </summary>
        /// <returns>The Main Page.</returns>
        public static Page GetMainPage()
        {
            // Register our views with our view models
            ViewFactory.Register<CameraPage, CameraViewModel>();
          
			var mainPage = new NavigationPage(new CameraViewPage());

            return mainPage;
        }
	}
}

