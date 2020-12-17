using WTAnalyzer.Views.ServicePages;
using Xamarin.Forms;

namespace WTAnalyzer
{
    public partial class App : Application
    {

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider
                .RegisterLicense("MzY0MzEwQDMxMzgyZTMzMmUzMFVzSFExZ0F2ekZDKzU0N0EwUHhlTmVhUkFEVHBad2ZaVFZqVHlzY1VrYlU9");
            InitializeComponent();
            MainPage = new NavigationPage(new StartupPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
