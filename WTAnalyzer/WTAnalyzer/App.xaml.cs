using System;
using WTAnalyzer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WTAnalyzer
{
    public partial class App : Application
    {

        public App()
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Helpers.License.Key());
            InitializeComponent();
            MainPage = new _TabPage();
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
