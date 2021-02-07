using Akavache;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WTAnalyzer.Resx;
using WTAnalyzer.Views.ServicePages;
using WTAnalyzer.XmlHandler;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WTAnalyzer
{
    public partial class App : Application
    {
        private bool isInternetConnected;
        private bool isDbCacheExist;

        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzczNjk4QDMxMzgyZTM0MmUzMGVyLzFjZ3RvTjlsSkN0bGtIdThqdnU1TnZwZjJSM0FZSGw5dmd3bUVtbWM9");

            Task.Run(CheckDbCache).Wait();
            Task.Run(CheckInternetConnection).Wait();

            if (isInternetConnected & isDbCacheExist)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new StartupPage();
            }
        }

        private async Task<bool> CheckDbCache()
        {
            isDbCacheExist = true;
            try
            {
                var arrayOfPlansCached = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
            }
            catch (KeyNotFoundException)
            {
                isDbCacheExist = false;
            }
            return isDbCacheExist;
        }

        private async Task<bool> CheckInternetConnection()
        {
            isInternetConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            return isInternetConnected;
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
