using WTAnalyzer.Views.ServicePages;
using Xamarin.Forms;

namespace WTAnalyzer
{
    public partial class App : Application
    {

        public App()
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Helpers.License.Key());
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
