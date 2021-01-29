using WTAnalyzer.Views.ServicePages;
using Xamarin.Forms;

namespace WTAnalyzer
{
    public partial class App : Application
    {

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider
                .RegisterLicense("MzczNjk4QDMxMzgyZTM0MmUzMGVyLzFjZ3RvTjlsSkN0bGtIdThqdnU1TnZwZjJSM0FZSGw5dmd3bUVtbWM9");
            InitializeComponent();
            MainPage = new AppShell();
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
